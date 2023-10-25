[aliases]
n0=/itemcheck { ; Final Fantasy Randomizer Item Magic Assigner. IRC Script by Linkshot
n1=  set %ffrspdefense Defense
n2=  set %ffrsplightaxe Light Axe
n3=  set %ffrsphealgear Heal Helm/Staff
n4=  set %ffrspmagestaff Mage Staff
n5=  set %ffrspthorzeus Thor/Zeus
n6=  set %ffrspwizardstaff Wizard Staff
n7=  set %ffrspblackshirt Black Shirt
n8=  set %ffrspbanesword Bane Sword
n9=  set %ffrspwhiteshirt White Shirt
n10=  set %ffrsppowerbonk Power Glove
n11=  :itemcheck
n12=  if (i isincs %ffrspflags) {
n13=    if (%ffrspitems != 1) {
n14=      if (%ffrsplightaxe != 1) && (%ffrspharm isnum) {
n15=        write -al $+ %ffrspharm SpellTable.txt [Light Axe]
n16=        var %ffrsplightaxe 1
n17=        goto itemcheck
n18=      }
n19=      if (%ffrsphealgear != 1) && (%ffrspgrouphug isnum) {
n20=        write -al $+ %ffrspgrouphug SpellTable.txt [Heal Helm/Staff]
n21=        var %ffrsphealgear 1
n22=        goto itemcheck
n23=      }
n24=      if (%ffrspthorzeus != 1) && (%ffrsplightning isnum) {
n25=        write -al $+ %ffrsplightning SpellTable.txt [Thor/Zeus]
n26=        var %ffrspthorzeus 1
n27=        goto itemcheck
n28=      }
n29=      if (%ffrspbanesword != 1) && (%ffrsptoxin isnum) {
n30=        write -al $+ %ffrsptoxin SpellTable.txt [Bane Sword]
n31=        var %ffrspbanesword 1
n32=        goto itemcheck
n33=      }
n34=      if (%ffrspmagestaff != 1) && (%ffrspweakbomb isnum) {
n35=        write -al $+ %ffrspweakbomb SpellTable.txt [Mage Staff]
n36=        var %ffrspmagestaff 1
n37=        goto itemcheck
n38=      }
n39=      if (%ffrspblackshirt != 1) && (%ffrspbigbomb isnum) {
n40=        write -al $+ %ffrspbigbomb SpellTable.txt [Black Shirt]
n41=        var %ffrspblackshirt 1
n42=        goto itemcheck
n43=      }
n44=      if (%ffrspwizardstaff != 1) && (%ffrspdebuffer isnum) {
n45=        write -al $+ %ffrspdebuffer SpellTable.txt [Wizard Staff]
n46=        var %ffrspwizardstaff 1
n47=        goto itemcheck
n48=      }
n49=      if (%ffrsppowerbonk != 1) && (%ffrspbonk isnum) {
n50=        write -al $+ %ffrspbonk SpellTable.txt [Power Glove]
n51=        var %ffrsppowerbonk 1
n52=        goto itemcheck
n53=      }
n54=      if (%ffrspwhiteshirt != 1) && (%ffrspcoverall isnum) {
n55=        write -al $+ %ffrspcoverall SpellTable.txt [White Shirt]
n56=        var %ffrspwhiteshirt 1
n57=        goto itemcheck
n58=      }
n59=      if (%ffrspdefense != 1) && (%ffrspdefender isnum) {
n60=        write -al $+ %ffrspdefender SpellTable.txt [Defense]
n61=        var %ffrspdefense 1
n62=        goto itemcheck
n63=      }
n64=    }
n65=    if (%ffrspdefense = 1) && (%ffrsplightaxe = 1) && (%ffrsphealgear = 1) && (%ffrspmagestaff = 1) && (%ffrspthorzeus = 1) && (%ffrspwizardstaff = 1) && (%ffrspblackshirt = 1) && (%ffrspbanesword = 1) && (%ffrspwhiteshirt = 1) && (%ffrsppowerbonk = 1) {
n66=      echo 9 -s All Item Magic Assigned!
n67=      set %ffrspitems 1
n68=    }
n69=    else {
n70=      echo 4 -s Item Magic Incomplete!
n71=      write -a SpellTable.txt DEBUG FILE -- DO NOT USE
n72=      write -a SpellTable.txt Missing: %ffrspdefense $+ , %ffrsplightaxe $+ , %ffrsphealgear $+ , %ffrspmagestaff $+ , %ffrspthorzeus $+ , %ffrspwizardstaff $+ , %ffrspblackshirt $+ , %ffrspbanesword $+ , %ffrspwhiteshirt $+ , %ffrsppowerbonk
n73=      /timer 1 5 /spell %ffrspflags
n74=      echo 14 -s Taking a break to breathe...
n75=    }
n76=  }
n77=}
