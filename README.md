# Usage

Before you start: make sure that the Opc server is running and that you can ping it from your PC.

1. Checkout solution
2. Start Api with IIS Express for example
3. You will get the Swagger page with the url and its port at the top as well as the available routes at the bottom. Use Read and Write to interact with the Opc server.
![image](https://github.com/oscarfonseca/opc-blockly-Bridge/assets/4384944/5d4ddb47-95b8-40db-a0d9-830081edcf86)

4. Install [Postman](https://www.postman.com/downloads/)
5. Start Postman and click **Import**  
   ![image](https://github.com/oscarfonseca/opc-blockly-Bridge/assets/4384944/90b637cc-e3d5-4be1-807f-533188f57e40)  
6. Select the file ```\Postman\Opc.postman_collection.json```
7. Use Read and Write requests from the collection to test the API.  
   Change the port according to step 3.  
   Notice that there is a Body provided in both cases (Read and Write).  
   ![image](https://github.com/oscarfonseca/opc-blockly-Bridge/assets/4384944/49ed37b5-0a1e-4bd3-89e9-5a8fd06ab7fb)
   ![image](https://github.com/oscarfonseca/opc-blockly-Bridge/assets/4384944/c5f74b26-5f0a-4a12-8f70-87c2033ef933)
   ![image](https://github.com/oscarfonseca/opc-blockly-Bridge/assets/4384944/2310156f-f13c-493a-9e28-60f4b838288a)





   
