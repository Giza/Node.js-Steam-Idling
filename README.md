### Node.js - node-steam

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

### Module
Premièrement, `require` module.
```js
var Steam = require('steam');
```


