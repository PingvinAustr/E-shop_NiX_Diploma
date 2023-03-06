# E-shop_NiX_Diploma
Using
 - .NET Core
 - HTML/CSS/JS
 - MVC
 - Docker
 - Redis
 - Identity server


## Description
This project is graduation project from NiX education center. It aims to consolidate knowledge in microservice architecture creation as well as in usage of technologies that are mentioned above.
Project has several microservices - Catalog API, MVC, Identity Server, Basket. All service methods are covered with unit tests.

The whole project is set up in docker. 
To run the project you need to execute "docker-compose up" in root directory.
You also need to update "hosts" file on your PC  (https://www.nublue.co.uk/guides/edit-hosts-file/#:~:text=In%20Windows%2010%20the%20hosts,%5CDrivers%5Cetc%5Chosts):
Paste these lines:
``` 
127.0.0.1 www.alevelwebsite.com
0.0.0.0 www.alevelwebsite.com
192.168.0.1 www.alevelwebsite.com 
