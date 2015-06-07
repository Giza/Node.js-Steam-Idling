    var Steam = require('steam');
    var fs = require('fs');
    var bot = new Steam.SteamClient();
     
    if (fs.existsSync('sentryfile'))
    {
        var sentry = fs.readFileSync('sentryfile');
        console.log('[STEAM] connexion avec sentry ');
        bot.logOn({
          accountName: '',
          password: '',
          shaSentryfile: sentry
        });
    }
    else
    {
        console.log('[STEAM] connecté sans sentry');
        bot.logOn({
          accountName: '',
          password: '',
          authCode: ''
        });
    }
    bot.on('loggedOn', function() {
        console.log('[STEAM] Connecté.');
        bot.setPersonaState(Steam.EPersonaState.Online);
        //Tell steam we are playing games.
        //440=tf2
        //550=l4d2 
        //730=csgo
        //570=dota2
        bot.gamesPlayed([271590, 550, 730, 570, 290080]);
    });
     
    bot.on('sentry', function(sentryHash)
    {//A sentry file is a file that is sent once you have
    //passed steamguard verification.
        console.log('[STEAM] Sentry file reçu.');
        fs.writeFile('sentryfile',sentryHash,function(err) {
        if(err){
          console.log(err);
        } else {
          console.log('[FS] Sauvegarde Sentry File.');
        }});
    });
     
    //Handle logon errors
    bot.on('error', function(e) {
    console.log('[STEAM] ERREUR - Connexion impossible');
        if (e.eresult == Steam.EResult.InvalidPassword)
        {
        console.log('Raison: Mot de passe invalide');
        }
        else if (e.eresult == Steam.EResult.AlreadyLoggedInElsewhere)
        {
        console.log('Raison: Déjà connecté');
        }
        else if (e.eresult == Steam.EResult.AccountLogonDenied)
        {
        console.log('Raison: Connexion bloqué - steam guard');
        }
    })
