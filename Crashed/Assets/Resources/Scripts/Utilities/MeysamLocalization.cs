using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;
using TMPro;

public static class MeysamLocalization
{
    static Dictionary<string, string> persianLocals = GeneratePersianLocalizedStrings();
    static Dictionary<string, string> englishLocals = GenerateEnglishLocalizedStrings();

    //Load a text asset file (Assets/Resources/Text/textFile01.txt)
    static TMP_FontAsset FontAssetEnglish = Resources.Load<TMP_FontAsset>("Fonts/COMIC SDF");
    static TMP_FontAsset FontAssetPersian = Resources.Load<TMP_FontAsset>("Fonts/Far_Bold SDF");

    private static Dictionary<string, string> GeneratePersianLocalizedStrings()
    {
        //for persian language
        Dictionary<string, string> localizeStrings = new Dictionary<string, string>();

        //main menu buttons
        localizeStrings.Add("Continue", "ﺍﺩﺍﻣﻪ");
        localizeStrings.Add("New Game", "ﺟﺪﯾﺪ");
        localizeStrings.Add("Website", "ﻭﺏ ﺳﺎﯾﺖ");
        localizeStrings.Add("Instagram", "ﺍﯾﻨﺴﺘﺎﮔﺮﺍﻡ");
        localizeStrings.Add("Exit", "ﺧﺮﻭﺝ");
        localizeStrings.Add("Version", Fa.faConvert(GameController.showVersion));
        localizeStrings.Add("Title", "ﺳﻘﻮﻁ ﮐﺮﺩﻩ: ﮔﻤﺸﺪﻩ ﺩﺭ ﺟﻨﮕﻞ");

        //in-game menu 
        localizeStrings.Add("Restart", "ﺁﻏﺎﺯ ﺩﻭﺑﺎﺭﻩ");
        localizeStrings.Add("Help", "ﺭﺍﻫﻨﻤﺎﯾﯽ");
        localizeStrings.Add("ReturnToMainMenu", "ﻣﻨﻮﯼ ﺍﺻﻠﯽ");

        //misc
        localizeStrings.Add("Alert!", "ﺍﺧﻄﺎﺭ!");
        localizeStrings.Add("Are you sure?", "ﺍﻃﻤﯿﻨﺎﻥ ﺩﺍﺭﯼ؟");
        localizeStrings.Add("Yes", "ﺁﺭﻩ");
        localizeStrings.Add("No thanks", "ﻧﻪ ﺑﯿﺨﯿﺎﻝ");
        localizeStrings.Add("Are you sure? You will lose all of your saved progress!", "ﺍﻃﻤﯿﻨﺎﻥ ﺩﺍﺭﯼ؟ ﺗﻤﺎﻡ ﻣﺮﺍﺣﻞ ﮐﻪ ﺭﻓﺘﯽ ﺍﺯ ﺩﺳﺖ ﻣﯿﺮﻩ!");
        localizeStrings.Add("It's a turn-base game!\n Tap on the screen to move around.\n You can kill the enemies with a spear.\n You can destroy rocks with a pickaxe.\n You can remove bushes with an axe.\n Pay attention to your energy!\n By consuming meat, you will get hyped and can move double the distance on the game by touch and hold!", "ﺑﺮﺍﯼ ﺣﺮﮐﺖ ﺻﻔﺤﻪ ﺭﻭ ﻟﻤﺲ ﮐﻨﯿﺪ.\nﻣﯽ ﺗﻮﺍﻧﯿﺪ ﺩﺷﻤﻨﺎﻥ ﺭﺍ ﺑﺎ ﻧﯿﺰﻩ ﺑﮑﺸﯿﺪ.\n ﺻﺨﺮﻩ ﻫﺎ ﺭﺍ ﻣﯽ ﺗﻮﺍﻧﯿﺪ ﺑﺎ ﭼﮑﺶ ﻧﺎﺑﻮﺩ ﮐﻨﯿﺪ.\n ﺩﺭﺧﺘﺎﻥ ﻭ ﺑﻮﺗﻪ ﻫﺎ ﺭﺍ ﺑﺎ ﺗﺒﺮ ﺍﺯ ﺳﺮ ﺭﺍﻩ ﺑﺮﺩﺍﺭﯾﺪ.\n ﺣﻮﺍﺳﺘﻮﻥ ﺑﻪ ﺍﻧﺮﮊﯾﺘﻮﻥ ﺑﺎﺷﻪ!\n ﺑﺎ ﺧﻮﺭﺩﻥ ﮔﻮﺷﺖ، ﺷﻤﺎ ﺑﻪ ﺣﺎﻟﺖ ﻫﺎﯾﭗ ﻭﺍﺭﺩ ﻣﯽ ﺷﻮﯾﺪ ﻭ ﻣﯽ ﺗﻮﺍﻧﯿﺪ ﺑﺎ ﺗﺎچ ﻭ ﻧﮕﻪ ﺩﺍﺷﺘﻦ ﺩﻭ ﺑﺮﺍﺑﺮ ﺣﺎﻟﺖ ﻋﺎﺩﯼ ﺣﺮﮐﺖ ﮐﻨﯿﺪ.");
        localizeStrings.Add("Got It!", "ﺣﻠﻪ!");
        localizeStrings.Add("Error!", "ﺧﻄﺎ!");
        localizeStrings.Add("You escaped!", "ﺷﻤﺎ ﺑﺎﻻﺧﺮﻩ ﻓﺮﺍﺭ ﮐﺮﺩﯾﺪ!");
        localizeStrings.Add("You got caught!", "ﮔﺮﻓﺘﺎﺭ ﺗﻠﻪ ﺷﺪﯾﺪ!");
        localizeStrings.Add("Can't remove it with this tool!", "ﺑﺎ ﺍﯾﻦ ﺍﺑﺰﺍﺭ ﻧﻤﯿﺸﻪ ﺣﺬﻓﺶ ﮐﺮﺩ!");
        localizeStrings.Add("Can't Kill it with this tool!", "ﺑﺎ ﺍﯾﻦ ﺍﺑﺰﺍﺭ ﻧﻤﯿﺸﻪ ﺣﻤﻠﻪ ﮐﺮﺩ!");
        localizeStrings.Add("You malnourished!", "ﻣﺮگ ﺍﺯ ﮔﺮﺳﻨﮕﯽ!");
        localizeStrings.Add("You died!", "ﻣﺮگ!");
        localizeStrings.Add("Credits", "ﻋﻨﺎﻭﯾﻦ");
        localizeStrings.Add("Please Wait...", "ﻟﻄﻔﺎ ﺻﺒﺮ ﮐﻨﯿﺪ...");
        localizeStrings.Add("Brought to you by Bored Alchemist indie game studio!\n Some of the musics and sounds by Eric Matyas:www.soundimage.org\n Take a look at our other games at: htttps://www.boredalchemist.com\n\n Programmer: Meysam Arab\n Art: Misagh Arab\n\n Thank you for playing the game!", "ﻃﺮﺍﺣﯽ ﻭ ﺗﻮﺳﻌﻪ ﺗﻮﺳﻂ ﺍﺳﺘﻮﺩﯾﻮﯼ ﮐﯿﻤﯿﺎﮔﺮ ﮐﺴﻞ. ﺑﺮﺧﯽ ﺍﺯ ﻣﻮﺳﯿﻘﯽ ﻫﺎ ﻭ ﺻﺪﺍﻫﺎ ﻣﺤﺼﻮﻟﯽ ﺍﺯ  gro.egamidnuos.www:saytaM cirE ﯾﻪ ﻧﮕﺎﻩ ﺑﻪ ﺑﺎﺯﯼ ﻫﺎﯼ ﺩﯾﮕﻪ ﻣﺎ ﺑﻨﺪﺍﺯﯾﺪ:  moc.tsimehcladerob.www//:spttth ﺑﺮﻧﺎﻣﻪ ﻧﻮﯾﺲ: ﻣﯿﺜﻢ ﻋﺮﺏ. ﺁﺭﺕ: ﻣﯿﺜﺎﻕ ﻋﺮﺏ. ﺳﭙﺎﺱ ﺍﺯ ﺷﻤﺎ");
        localizeStrings.Add("How it all started", "ﻣﺎﺟﺮﺍ ﺍﺯ ﮐﺠﺎ ﺷﺮﻭﻉ ﺷﺪ");
        localizeStrings.Add("Our plane CRASHED in the middle of nowhere! I'm alone, scared, and hungry. I must find a way out of here alive.", "ﻫﻮﺍﭘﯿﻤﺎ ﻭﺳﻂ ﻧﺎﮐﺠﺎﺁﺑﺎﺩ ﺳﻘﻮﻁ ﮐﺮﺩ. ﺗﻨﻬﺎ ﻫﺴﺘﻢ ﻭ ﻭﺣﺸﺖ ﺯﺩﻩ ﻭ ﺑﺪﻭﻥ ﺁﺏ ﻭ ﻏﺬﺍ. ﺑﺎﯾﺪ ﺳﻌﯽ ﮐﻨﻢ ﺍﺯ ﺍﯾﻦ ﺍﻭﺿﺎﻉ ﺯﻧﺪﻩ ﺑﯿﺮﻭﻥ ﺑﺮﻡ.");
        localizeStrings.Add("With an Axe, I can cut bushes and cut down trees!", "ﺑﺎ ﺗﺒﺮ ﻓﻘﻂ ﻣﯿﺘﻮﻧﻢ ﺩﺭﺧﺖ ﻫﺎ ﺭﻭ ﻗﻄﻊ ﮐﻨﻢ ﻭ ﺑﻮﺗﻪ ﻫﺎ ﺭﻭ ﺑﺒﺮﻡ!");
        localizeStrings.Add("With a pickaxe, I can destroy boulders!", "ﺑﺎ ﮐﻠﻨﮓ ﻓﻘﻂ ﻣﯿﺘﻮﻧﻢ ﺻﺨﺮﻩ ﻫﺎ ﺭﻭ ﺗﺨﺮﯾﺐ ﮐﻨﻢ!");
        localizeStrings.Add("With a Spear, I can defend myself or hunt animals!", "ﺑﺎ ﻧﯿﺰﻩ ﻓﻘﻂ ﻣﯿﺘﻮﻧﻢ ﺍﺯ ﺧﻮﺩﻡ ﺩﻓﺎﻉ ﮐﻨﻢ ﯾﺎ ﺣﯿﻮﺍﻧﺎﺕ ﺩﯾﮕﻪ ﺭﻭ ﺷﮑﺎﺭ ﮐﻨﻢ!");



        //speechs
        localizeStrings.Add("Damn it!", "ﻟﻌﻨﺖ ﺑﻪ ﺷﯿﻄﻮﻥ!");
        localizeStrings.Add("That hurts!", "ﺍﻭﺥ");
        localizeStrings.Add("Ahhhh", "ﺁﻫﻬﻬﻪ");
        localizeStrings.Add("Aghhh", "ﺁﺥ!");
        localizeStrings.Add("Thanks man!", "ﻣﻤﻨﻮﻥ");
        localizeStrings.Add("Wow! How generous of you!", "ﻭﺍﻭ! ﭼﻘﺪﺭ ﺩﺳﺖ ﻭ ﺩﻟﺒﺎﺯﯼ");
        localizeStrings.Add("That hits the spot!", "ﺧﯿﻠﯽ ﺣﺎﻝ ﺩﺍﺩ");
        localizeStrings.Add("Wow!", "ﻭﺍﻭ!");
        localizeStrings.Add("It's too hot!", "ﺧﯿﻠﯽ ﮔﺮﻣﻪ!");
        localizeStrings.Add("Too many bugs!", "ﭘﺸﻪ ﺗﯿﮑﻪ ﭘﺎﺭﻡ ﮐﺮﺩ!");
        localizeStrings.Add("Can i survive this mess?", "ﻣﯽ ﺗﻮﻧﻢ ﺯﻧﺪﻩ ﺩﺭ ﺑﺮﻡ؟");
        localizeStrings.Add("What was that?", "ﭼﯽ ﺑﻮﺩ؟");
        localizeStrings.Add("What should i do?", "ﺣﺎﻻ ﭼﯿﮑﺎﺭ ﮐﻨﻢ؟");
        localizeStrings.Add("Low energy! I must either pick something to eat or watch some video ads!", "ﺍﻧﺮﮊﯾﻢ ﮐﻤﻪ! ﺑﻬﺘﺮﻩ ﯾﺎ ﯾﻪ ﭼﯿﺰﯼ ﺑﺮﺍﯼ ﺧﻮﺭﺩﻥ ﭘﯿﺪﺍ ﮐﻨﻢ ﯾﺎ ﯾﻪ ﻭﯾﺪﯾﻮ ﻧﮕﺎﻩ ﮐﻨﻢ ﯾﺎ ﯾﮑﻢ ﺍﻧﺮﮊﯼ ﺑﮕﯿﺮﻡ!");
        localizeStrings.Add("I'm Low on energy! Better to watch some video ads so I can get some!", "ﺍﻧﺮﮊﯾﻢ ﺧﯿﻠﯽ ﮐﻤﻪ! ﯾﻪ ﻭﯾﺪﯾﻮ ﻧﮕﺎﻩ ﮐﻨﻢ ﺷﺎﯾﺪ ﯾﮑﻢ ﺟﻮﻥ ﺑﮕﯿﺮﻡ!");

        //ingame
        localizeStrings.Add("New Day!", "ﺭﻭﺯ ﺟﺪﯾﺪ!");
        localizeStrings.Add("Day ", "روز ");
        localizeStrings.Add("Energy: ", "ﺍﻧﺮﮊﯼ: ");
        localizeStrings.Add("Remaining Hype: ", "ﻫﯿﺠﺎﻥ: ");
        localizeStrings.Add("Energy + ", "ﺍﻧﺮﮊﯼ + ");

        //map
        localizeStrings.Add("Discovery!", "ﮐﺸﻒ!");
        localizeStrings.Add("Wow! It's some sort of map! Maybe by collecting all the parts i can find a way to get out of here!", "ﻭﺍﻭ! ﻗﺴﻤﺘﯽ ﺍﺯ ﯾﻪ ﻧﻘﺸﻪ! ﺷﺎﯾﺪ ﺍﮔﺮ ﻧﻘﺸﻪ ﺭﻭ ﮐﺎﻣﻞ ﮐﻨﻢ ﺑﺘﻮﻧﻢ ﺍﺯ ﺟﻨﮕﻞ ﺧﺎﺭﺝ ﺑﺸﻢ!");
        localizeStrings.Add("Congratulations!", "ﺗﺒﺮﯾﮏ!");
        localizeStrings.Add("You completed the map! Now you can get out of this cursed jungle easly!", "ﻧﻘﺸﻪ ﮐﺎﻣﻞ ﺷﺪ! ﺑﺎﻻﺧﺮﻩ ﻣﯽ ﺗﻮﻧﯽ ﺍﺯ ﺍﯾﻦ ﺟﻨﮕﻞ ﻧﻔﺮﯾﻦ ﺷﺪﻩ ﺧﺎﺭﺝ ﺑﺸﯽ!");

        //items and energies
        localizeStrings.Add("Meat", "ﮔﻮﺷﺖ");
        localizeStrings.Add("Banana", "ﻣﻮﺯ");
        localizeStrings.Add("Coconut", "ﻧﺎﺭﮔﯿﻞ");
        localizeStrings.Add("Pineapple", "ﺁﻧﺎﻧﺎﺱ");
        localizeStrings.Add("Axe", "ﺗﺒﺮ");
        localizeStrings.Add("Pickaxe", "ﮐﻠﻨﮓ");
        localizeStrings.Add("Spear", "ﻧﯿﺰﻩ");

        //blockades
        localizeStrings.Add("Boulder", "ﺗﺨﺘﻪ ﺳﻨﮓ");
        localizeStrings.Add("Bush", "ﺑﻮﺗﻪ");
        localizeStrings.Add("Pit", "ﭼﺎﻩ");
        localizeStrings.Add("BananaTree", "ﺩﺭﺧﺖ ﻣﻮﺯ");
        localizeStrings.Add("CoconutTree", "ﺩﺭﺧﺖ ﻧﺎﺭﮔﯿﻞ");
        localizeStrings.Add("Spike", "ﺍﺳﭙﺎﯾﮏ");
        localizeStrings.Add("PineapplePlant", "ﮔﯿﺎﻩ ﺁﻧﺎﻧﺎﺱ");


        return localizeStrings;
    }

    private static Dictionary<string, string> GenerateEnglishLocalizedStrings()
    {
        //for english language
        Dictionary<string, string> localizeStrings = new Dictionary<string, string>();

        //main menu buttons
        localizeStrings.Add("Continue", "Continue");
        localizeStrings.Add("New Game", "New Game");
        localizeStrings.Add("Website", "Website");
        localizeStrings.Add("Instagram", "Instagram");
        localizeStrings.Add("Exit", "Exit");
        localizeStrings.Add("Version", GameController.showVersion);
        localizeStrings.Add("Title", "Crashed: Lost in Jungle");

        //ingame menu 
        localizeStrings.Add("Restart", "Restart");
        localizeStrings.Add("Help", "Help");
        localizeStrings.Add("ReturnToMainMenu", "Main Menu");

        //misc
        localizeStrings.Add("Alert!", "Alert!");
        localizeStrings.Add("Are you sure?", "Are you sure?");
        localizeStrings.Add("Yes", "Yes");
        localizeStrings.Add("No thanks", "No thanks");
        localizeStrings.Add("Are you sure? You will lose all of your saved progress!", "Are you sure ? You will lose all of your saved progress!");
        localizeStrings.Add("It's a turn-base game!\n Tap on the screen to move around.\n You can kill the enemies with a spear.\n You can destroy rocks with a pickaxe.\n You can remove bushes with an axe.\n Pay attention to your energy!\n By consuming meat, you will get hyped and can move double the distance on the game by touch and hold!", "It's a turn-base game!\n Tap on the screen to move around.\n You can kill the enemies with a spear.\n You can destroy rocks with a pickaxe.\n You can remove bushes with an axe.\n Pay attention to your energy!\n By consuming meat, you will get hyped and can move double the distance on the game by touch and hold!");
        localizeStrings.Add("Got It!", "Got It!");
        localizeStrings.Add("Error!", "Error!");
        localizeStrings.Add("You escaped!", "You escaped!");
        localizeStrings.Add("You got caught!", "You got caught!");
        localizeStrings.Add("Can't remove it with this tool!", "Can't remove it with this tool!");
        localizeStrings.Add("Can't Kill it with this tool!", "Can't Kill it with this tool!");
        localizeStrings.Add("You malnourished!", "You malnourished!");
        localizeStrings.Add("You died!", "You died!");
        localizeStrings.Add("Credits", "Credits");
        localizeStrings.Add("Brought to you by Bored Alchemist indie game studio!\n Some of the musics and sounds by Eric Matyas:www.soundimage.org\n Take a look at our other games at: htttps://www.boredalchemist.com\n\n Programmer: Meysam Arab\n Art: Misagh Arab\n\n Thank you for playing the game!", "Brought to you by Bored Alchemist indie game studio!\n Some of the musics and sounds by Eric Matyas:www.soundimage.org\n Take a look at our other games at: htttps://www.boredalchemist.com\n\n Programmer: Meysam Arab\n Art: Misagh Arab\n\n Thank you for playing the game!");
        localizeStrings.Add("Please Wait...", "Please Wait...");
        localizeStrings.Add("How it all started", "How it all started");
        localizeStrings.Add("Our plane CRASHED in the middle of nowhere! I'm alone, scared, and hungry. I must find a way out of here alive.", "Our plane CRASHED in the middle of nowhere! I'm alone, scared, and hungry. I must find a way out of here alive.");
        localizeStrings.Add("With an Axe, I can cut bushes and cut down trees!", "With an Axe, I can cut bushes and cut down trees!");
        localizeStrings.Add("With a pickaxe, I can destroy boulders!", "With a pickaxe, I can destroy boulders!");
        localizeStrings.Add("With a Spear, I can defend myself or hunt animals!", "With a Spear, I can defend myself or hunt animals!");
  

        //speechs
        localizeStrings.Add("Damn it!", "Damn it!");
        localizeStrings.Add("That hurts!", "That hurts!");
        localizeStrings.Add("Ahhhh", "Ahhhh");
        localizeStrings.Add("Aghhh", "Aghhh");
        localizeStrings.Add("Thanks man!", "Thanks man!");
        localizeStrings.Add("Wow! How generous of you!", "Wow! How generous of you!");
        localizeStrings.Add("That hits the spot!", "That hits the spot!");
        localizeStrings.Add("Wow!", "Wow!");
        localizeStrings.Add("It's too hot!", "It's too hot!");
        localizeStrings.Add("Too many bugs!", "Too many bugs!");
        localizeStrings.Add("Can i survive this mess?", "Can i survive this mess?");
        localizeStrings.Add("What was that?", "What was that?");
        localizeStrings.Add("What should i do?", "What should i do?");
        localizeStrings.Add("Low energy! I must either pick something to eat or watch some video ads!", "Low energy! I must either pick something to eat or watch some video ads!");
        localizeStrings.Add("I'm Low on energy! Better to watch some video ads so I can get some!", "I'm Low on energy! Better to watch some video ads so I can get some!");


        //ingame
        localizeStrings.Add("New Day!", "New Day!");
        localizeStrings.Add("Day ", "Day ");
        localizeStrings.Add("Energy: ", "Energy: ");
        localizeStrings.Add("Remaining Hype: ", "Hype: ");
        localizeStrings.Add("Energy + ", "Energy + ");

        //map
        localizeStrings.Add("Discovery!", "Discovery!");
        localizeStrings.Add("Wow! It's some sort of map! Maybe by collecting all the parts i can find a way to get out of here!", "Wow! It's some sort of map! Maybe by collecting all the parts i can find a way to get out of here!");
        localizeStrings.Add("Congratulations!", "Congratulations!");
        localizeStrings.Add("You completed the map! Now you can get out of this cursed jungle easly!", "You completed the map! Now you can get out of this cursed jungle easly!");

        //items and energies
        localizeStrings.Add("Meat", "Meat");
        localizeStrings.Add("Banana", "Banana");
        localizeStrings.Add("Coconut", "Coconut");
        localizeStrings.Add("Pineapple", "Pineapple");
        localizeStrings.Add("Axe", "Axe");
        localizeStrings.Add("Pickaxe", "Pickaxe");
        localizeStrings.Add("Spear", "Spear");

        //blockades
        localizeStrings.Add("Boulder", "Boulder");
        localizeStrings.Add("Bush", "Bush");
        localizeStrings.Add("Pit", "Pit");
        localizeStrings.Add("BananaTree", "BananaTree");
        localizeStrings.Add("CoconutTree", "CoconutTree");
        localizeStrings.Add("Spike", "Spike");
        localizeStrings.Add("PineapplePlant", "PineapplePlant");
     

        return localizeStrings;
    }

    public static string GetLocalizaStringByKey(string key)
    {
        if (string.IsNullOrEmpty(key))
            return string.Empty;

        if (GameController.language == "pr")
            return persianLocals[key];
        else
            return englishLocals[key];

    }

    public static TMP_FontAsset GetCurrentAllowedFontAsset()
    {
        if (GameController.language == "pr")
            return FontAssetPersian;
        else
            return FontAssetEnglish;
       
    }

    public static bool IsCurrentLanguageRTL()
    {
        if (GameController.language == "pr")
            return true;
        else
            return false;

    }

    public static string GetLocalizaNumberStringByLanguage(int number)
    {
        if (GameController.language == "pr")
        {

            char[] charArray = number.ToString().ToCharArray();
            Array.Reverse(charArray);
            return Fa.faConvert(new string(charArray));
        }
        else
            return number.ToString();

    }
}


