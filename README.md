# CryptoPortfolio
Crypto Currency Portfolio Tracker made in .Net Core and Angular 8

Test Project I created to learn .Net Core web API and Angular 8.



<p align="center">
  <b> Main View </b>
  <img src="images/MainView.png">
  <b> Portfolio View </b> 
  <img src="images/Portfolio.png"> 
  <b> Add Transaction </b>
</p>
<img src="images/AddTransaction.png">


FEATURES
--------

* view top 200 Cryptocurrencies
* changes in value over 1hr, 24hr, 7 days
* view on different screen sizes mobile/desktop (Bootstrap)
* Auto Refreshes the price every 2 minutes
* Register new user
* Login/Logout
* Create new portfolio
* View portfolio
* Add coin to portfolio
* Add new buy/sell transaction
* View pie chart of current portfolio
* View total amount of all portfolios
* View Total amount and value of all coins in all portfolios
* included fear and greed index from https://alternative.me/crypto/fear-and-greed-index

Soon to be included..
* View transactions by portfolio/coin
* Sort by price change / price / circulating suppoy
* Alerts for price notifications


I get the price data from - https://coinmarketcap.com/api/


Tech / Framworks / Languages used
---------------------------------

<b>.Net Core</b>

* Entity framework for db storing users portfolios coins and transactions
* Postman used to test API calls and responses
* gets prices every two minutes from coinmarketcap api and stores in localy that can be accessed from front end without extra API calls.
* Handles user register and logon using salt and hashing

<b>Angular 8</b>

* TypeScript
* ChartJS - display pie charts 
* CSS
* Bootstrap - used to display items correctly for both mobile and desktop
* ngx bootstrap - for date picker and button selectors.
* Alertifyjs - notifications for logins / successfull registrations etc

Don't store any real cryptocurrency portfolios on this site!. - https://cryfolio.azurewebsites.net/
