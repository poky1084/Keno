from flask import Flask, request, jsonify, Response
from curl_cffi import requests
import asyncio
from curl_cffi.requests import AsyncSession
import json
import urllib.parse

app = Flask(__name__)

class CurlCffiProxy:
    def __init__(self):
        self.supported_browsers = ["chrome", "safari", "edge", "firefox"]
    
    def make_request(self, url, method="GET", headers=None, data=None, 
                   impersonate="chrome", proxies=None, timeout=30):
        """Make a request using curl_cffi with browser impersonation"""
        try:
            method = method.upper()
            request_args = {
                "url": url,
                "impersonate": impersonate,
                "timeout": timeout,
                "headers": headers or {}
            }
            
            if proxies:
                request_args["proxies"] = proxies
            
            if method == "GET":
                response = requests.get(**request_args)
            elif method == "POST":
                request_args["data"] = data
                response = requests.post(**request_args)
            elif method == "PUT":
                request_args["data"] = data
                response = requests.put(**request_args)
            elif method == "DELETE":
                response = requests.delete(**request_args)
            else:
                return {"error": f"Unsupported method: {method}"}
            
            # Return response data in a serializable format
            return {
                "status_code": response.status_code,
                "headers": dict(response.headers),
                "content": response.text,
                "url": str(response.url),
                "success": True
            }
            
        except Exception as e:
            return {"error": str(e), "success": False}

proxy_handler = CurlCffiProxy()

# Enhanced POST endpoint with better GraphQL handling
@app.route('/graphql', methods=['POST'])
def handle_graphql_proxy():
    """Specialized endpoint for GraphQL requests"""
    try:
        # Get data from JSON body
        data = request.get_json()
        
        #if not data or 'url' not in data:
            #return jsonify({"error": "URL is required in JSON body", "success": False})
        print(data)
        url = request.args.get('url')
        query = data.get('query', '')
        variables = data.get('variables', {})
        operation_name = data.get('operationName')
        impersonate = data.get('impersonate', 'chrome')
        token = data.get('token')
        
        # Build proper GraphQL request
        graphql_payload = {
            "query": query,
            "variables": variables
        }
        
        if operation_name:
            graphql_payload["operationName"] = operation_name
        
        # Default headers for GraphQL
        headers = data.get('headers', {})
        
        headers['x-access-token'] = token
        
        if 'Content-Type' not in headers:
            headers['Content-Type'] = 'application/json'
        if 'User-Agent' not in headers:
            headers['User-Agent'] = 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36'
        
        # Make the request
        result = proxy_handler.make_request(
            url=url,
            method="POST",
            headers=headers,
            data=json.dumps(graphql_payload),
            impersonate=impersonate,
            timeout=30
        )
        print(result['content'])
        return result['content']
        
    except Exception as e:
        return jsonify({"error": str(e), "success": False})

@app.route('/')
def index():
    """Home page with instructions"""
    return """
    <html>
        <head>
            <title>Python Proxy Server</title>
            <style>
                body { font-family: Arial, sans-serif; margin: 40px; }
                code { background: #f4f4f4; padding: 10px; display: block; margin: 10px 0; }
            </style>
        </head>
        <body>
            <h1>Python Proxy Server with curl_cffi</h1>
            
            <h2>Usage Examples:</h2>
            
            <h3>1. Simple GET (Browser Address Bar):</h3>
            <code>http://localhost:5000/get?url=https://httpbin.org/json</code>
            
            <h3>2. With Browser Impersonation:</h3>
            <code>http://localhost:5000/get?url=https://httpbin.org/json&impersonate=safari</code>
            
            <h3>3. View Content Directly:</h3>
            <code>http://localhost:5000/fetch?url=https://httpbin.org/html</code>
            
            <h3>4. POST Requests (use tools like curl or C#):</h3>
            <code>POST http://localhost:5000/proxy</code>
            <code>Content-Type: application/json</code>
            <code>{"url": "https://httpbin.org/post", "method": "POST", "data": "test=data"}</code>
            
            <br>
            <p><strong>Supported browsers:</strong> chrome, safari, edge, firefox</p>
        </body>
    </html>
    """

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)