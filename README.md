# SelfCheckout_DT

Unfortunately, the Checkout endpoint has not been completed, I apologize for that.



Testing:
- Open the Project in Visual Studio 2022
+ F5 -> Run

- Download Postman (https://www.postman.com/downloads/) - without registration
- GET and POST request can be sent
+ With GET: https://localhost:<port>/app/v1/Stock
    - You can see <port> on the page, whick was launched by Visual Studio
+ With POST: https://localhost:7213/app/v1/Stock
    - Write into Body a correct JSON array e.g: [ {"Value": "1000", "Amount": "1"}, {"Value": "500", "Amount": "0"} ]
+ With POST: https://localhost:7213/app/v1/Checkout
    - Write into Body a correct Checkout object e.g: { inserted: [ {"Value": "1000", "Amount": "1"}, {"Value": "500", "Amount": "0"} ], "price": "1500" }
 

- Checkout planned:
    - Sort: DESC by Values (!)
    - Run over Denominations 
      - if Value < price   //stay by value as soon as value < price
        - price -= Value
    + // The small denominations remain this way until they have to be returned.
  

  
  
Ziaja Bálint
