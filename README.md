### Node.js - node-steam [![Node.js](http://viewsame.com/img/nodejs-icon.png)](https://nodejs.org/)[![steam-ico](http://icons.iconarchive.com/icons/bokehlicia/captiva/48/steam-icon.png)](http://icons.iconarchive.com)

node-steam est une alternative au SteamKit2. Il vous permet d'intéragir avec le réseau Steam sans avoir de client steam. 
Souvent utilisé pour des tâches automatisés ou des bot. 

### Requièrement
Vous devez installer : 
- [Node.js](https://nodejs.org/)
- [node-steam](https://github.com/seishun/node-steam)
- [Node.js-Steam-Idling](https://github.com/NastyZ98/Node.js-Steam-Idling)

### Installation

Pour pouvoir utiliser Node.js-Steam-Idling vous devez installer [Node.js](https://nodejs.org/).
Ouvez "Node.js command prompt" et saisissez
```
npm install steam
```
Note: Le chemin d'accès sera le suivant:
```
node-modules/steam/
```
Installez "Idling.js" dans:
```
node-modules/steam/Idling.js
```
Créez un fichier batch
```batch
cd C:\User\User\node-modules\steam\
node Idling.js
```

### Synthax requise en début de fichier
Premièrement, `require` module.
```js
var Steam = require('steam');
```
`steam` est maintenant un espace de nom (implémenté comme objet) il contient la class SteamClient, la propriété `servers`, et une grande collection d'instances sont implémentés comme objet.

Si l'on veut créer une instance de SteamClient, on appel la méthode `logON`
```js
var bot = new Steam.SteamClient();
bot.logOn({
  accountName: 'username',
  password: 'password'
});
bot.on('loggedOn', function() { /* ... */});
```





