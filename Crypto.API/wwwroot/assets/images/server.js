var fs = require('fs'),
http = require('http'),
https = require('https');
  
var Stream = require('stream').Transform;
  
var downloadImageFromURL = (url, filename, callback) => {
  
    var client = http;
    if (url.toString().indexOf("https") === 0){
      client = https;
     }
  
    client.request(url, function(response) {                                        
      var data = new Stream();                                                    
  
      response.on('data', function(chunk) {                                       
         data.push(chunk);                                                         
      });                                                                         
  
      response.on('end', function() {                                             
         fs.writeFileSync(filename, data.read());                               
      });                                                                         
   }).end();
};



downloadImageFromURL('https://assets.coingecko.com/coins/images/10840/large/logo_transparent_4x.png?1584623184', 'hive.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/5992/large/gemini-dollar-gusd.png?1536745278', 'gemini-dollar.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/63/large/digibyte.png?1547033717', 'digibyte.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/15279/large/casper.PNG?1620341020', 'casper-network.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/11636/large/rndr.png?1638840934', 'render-token.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/14316/large/54023228.png?1615366911', 'playdapp.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/10366/large/SLP.png?1578640057', 'smooth-love-potion.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/385/large/Lisk_Symbol_-_Blue.png?1573444104', 'lisk.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/3139/large/REN.png?1589985807', 'republic-protocol.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/17358/large/le1nzlO6_400x400.jpg?1632465691', 'yield-guild-games.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/794/large/Voyager-vgx.png?1575693595', 'ethos.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/9566/large/Nervos_White.png?1608280856', 'nervos-network.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/25244/large/OP.jpeg?1651026279', 'optimism.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/14899/large/RwdVsGcw_400x400.jpg?1618923851', 'kyber-network-crystal.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/794/large/Voyager-vgx.png?1575693595', 'ethos.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/13915/large/4.png?1612838831', 'dao-maker.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/20210/large/8glAYOTM_400x400.jpg?1636667919', 'vvs-finance.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/13928/large/PSigc4ie_400x400.jpg?1612875614', 'raydium.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/12811/large/barnbridge.jpg?1602728853', 'barnbridge.png');
downloadImageFromURL('https://assets.coingecko.com/coins/images/15823/large/RBN_64x64.png?1633529723', 'ribbon-finance.png');
