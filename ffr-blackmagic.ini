[aliases]
n0=/bmag { ; Final Fantasy Randomizer Black Magic Generator. IRC script by Linkshot
n1=  ; Black Spell Initiation
n2=  :blackmagic
n3=  write -c BlackSpell.txt
n4=  var %ffrbmstrskip 0
n5=  var %ffrbmaccskip 0
n6=  var %ffrbmeffect $rand(1,2)
n7=  var %ffrbmohko $rand(1,9)
n8=  var %ffrbmalter $rand(1,2)
n9=  var %ffrbmdebuff $rand(1,12)
n10=  var %ffrbmbuff $rand(1,5)
n11=  var %ffrbmelement $rand(0,8)
n12=  var %ffrbmtarget $rand(1,2)
n13=  var %ffrbmaccroll $rand(0,255)
n14=  var %ffrbmaccuracy $rand($calc((2^$rand(5,6))-1),$calc((2^$rand(1,8))-1))
n15=  var %ffrbmaccsay Undefined
n16=  if (%ffrspellbinding = 1) {
n17=    if (%ffrsplevel = 8) { var %ffrbmstrength 100 }
n18=    else { var %ffrbmstrength $calc( %ffrsplevel * 10) }
n19=  }
n20=  else { var %ffrbmstrength $rand(10,100) }
n21=  var %ffrbmafflict $rand(1,9)
n22=  var %ffrbmpoison $rand(1,5)  
n23=  :bmbase
n24=
n25=  ; -b2: Preserves all slots with damage spells
n26=  if (b2 isincs %ffrspflags) && (%ffrspreroll < 5) {
n27=    if ((%ffrspslot = 1) && (%ffrsplevel != 4)) || ((%ffrsplevel = 1) && (%ffrspslot = 4)) || ((%ffrsplevel = 3) && (%ffrspslot = 3)) || ((%ffrsplevel = 4) && (%ffrspslot = 4)) {
n28=      var %ffrbmeffect 1
n29=      var %ffrbmohko $rand(4,8)
n30=      var %ffrbmb2 1
n31=    }
n32=  }
n33=
n34=  ; Sets Accuracy to Auto-Hit
n35=  if (%ffrbmaccroll < 2) || (%ffrbmaccroll = 148) || (%ffrbmaccsay = Auto-Hit) {
n36=    if (%ffrbmeffect = 1) && (%ffrbmohko < 4) { echo 11 -s Debug: Rejected Auto-Hit on Insta-Kill }
n37=    else { var %ffrbmaccsay Auto-Hit }
n38=  }
n39=  elseif (%ffrspellbinding = 1) {
n40=    if (%ffrsplevel < 5) { var %ffrbmaccuracy 5 | set %ffrbmtier 2 }
n41=    if (%ffrsplevel > 4) { var %ffrbmaccuracy 7 | set %ffrbmtier 3 }
n42=    if (%ffrsplevel = 1) { var %ffrbmaccuracy 4 | set %ffrbmtier 1 }
n43=    if (%ffrsplevel = 8) { var %ffrbmaccuracy 9 | set %ffrbmtier 4 }
n44=  }
n45=  else { var %ffrbmaccsay %ffrbmaccuracy }
n46=
n47=  ; Ensure Confused ailment never casts a buff on self
n48=
n49=  if (C isincs %ffrspflags) && (%ffrspreroll < 5) && (%ffrsplevel = 1) && (%ffrspslot = 1) && (%ffrbmeffect = 2) && (%ffrbmalter = 2) {
n50=    if (%ffrbmbuff = 1) { var %ffrbmdebug ARM }
n51=    elseif (%ffrbmbuff = 2) { var %ffrbmdebug resistance }
n52=    elseif (%ffrbmbuff = 3) { var %ffrbmdebug FAST }
n53=    elseif (%ffrbmbuff = 4) { var %ffrbmdebug TMPR }
n54=    elseif (%ffrbmbuff = 5) { var %ffrbmdebug HIDE }
n55=    else { var %ffrbmdebug Undefined Buff }
n56=    echo 11 -s Debug: Bounced %ffrbmdebug from FIRE slot
n57=    goto blackmagic
n58=  }
n59=
n60=  ; Compatibility with the LOK2 fix
n61=
n62=  if (S isincs %ffrspflags) && (%ffrspreroll < 5) && (%ffrsplevel = 3) && (%ffrspslot = 4) { var %ffrbmeffect 2 | var %ffrbmalter 1 | var %ffrbmdebuff 10 }
n63=
n64=  ; Spell-building
n65=
n66=  ; Black Magic is half Offensive Spells
n67=  ; The other half is Stat Alterations
n68=
n69=  ; 3 in 8 Offensive Spells are Insta-Kills
n70=  ; They have a lowered chance to multi-target
n71=  ; However, Damage Spells have a boosted chance
n72=  if (%ffrbmeffect = 1) {
n73=    if (%ffrbmohko < 4) {
n74=      if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 2 }
n75=      var %ffrbmstrskip 1
n76=      dec %ffrbmtarget $rand(0,1)
n77=      write -al2 BlackSpell.txt Type: Insta-Kill
n78=    }
n79=    else {
n80=      inc %ffrbmtarget $rand(0,1)
n81=      write -al2 BlackSpell.txt Type: Damage
n82=      if (%ffrspellbinding = 1) {
n83=        if (%ffrsplevel = 2) { dec %ffrbmaccuracy 1 }
n84=        if (%ffrbmtarget = 1) && (%ffrsplevel = 8) { inc %ffrbmstrength 20 }
n85=      }
n86=      else {
n87=        if (%ffrbmtarget = 1) { var %ffrbmstrength $floor($calc( %ffrbmstrength * ($calc( $rand(100,150) /100)))) }
n88=      }
n89=      if (%ffrbmstrength isnum 30-49) { set %ffrbmtier 2 }
n90=      elseif (%ffrbmstrength isnum 50-99) { set %ffrbmtier 3 }
n91=      elseif (%ffrbmstrength >= 100) { set %ffrbmtier 4 }
n92=      else { set %ffrbmtier 1 }
n93=    }
n94=  }
n95=  ; Decides whether the Alteration is a buff or debuff
n96=  ; Buffs are rarer than Debuffs
n97=  elseif (%ffrbmeffect = 2) { 
n98=    if (%ffrbmalter = 1) { write -al2 BlackSpell.txt Type: Debuff }
n99=    elseif (%ffrbmalter = 2) {
n100=      if ($rand(0,1) = 0) {
n101=        dec %ffrbmalter 1
n102=        echo 11 -s Debug: Shifting Buff to Debuff
n103=        goto bmbase
n104=      }
n105=      else { echo 7 -s Debug: Buff passed saving roll ( $+ $v1 vs 0) }
n106=      write -al2 BlackSpell.txt Type: Buff
n107=    }
n108=    else { echo 4 -s Value out of range | /halt }
n109=  }
n110=  else { echo 4 -s Value out of range | /halt }
n111=
n112=  ; Stat Alterations
n113=
n114=  if (%ffrbmeffect = 2) {
n115=    if (%ffrbmalter = 1) {
n116=      ; Debug /echo 7 -s Debug: Passing through Debuff (Debuff: %ffrbmdebuff $+ , Element: %ffrbmelement $+ , Poison: %ffrbmpoison $+ )
n117=      if (%ffrspellbinding = 1) && (%ffrbmaccsay = Auto-Hit) && (%ffrbmdebuff < 8) { var %ffrbmdebuff 12 }
n118=      if (%ffrbmdebuff !isnum 9-10) { var %ffrbmstrskip 1 }
n119=      if (%ffrbmdebuff = 1) {
n120=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n121=        if (%ffrbmpoison = 1) { write -al3 BlackSpell.txt Effect: Dark & Poison }
n122=        elseif (%ffrbmpoison = 2) { write -al3 BlackSpell.txt Effect: Stun & Poison }
n123=        elseif (%ffrbmpoison = 3) { 
n124=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 1 }
n125=          if (%ffrsplevel > 1) { inc %ffrbmaccuracy 2 }
n126=          write -al3 BlackSpell.txt Effect: Sleep & Poison
n127=        }
n128=        elseif (%ffrbmpoison = 4) {
n129=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n130=          write -al3 BlackSpell.txt Effect: Mute & Poison
n131=        }
n132=        elseif (%ffrbmpoison = 5) {
n133=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 4 }
n134=          write -al3 BlackSpell.txt Effect: Confuse & Poison
n135=        }
n136=        ; Poison Debuff is always stacked on top
n137=        ; So that the spell is useful to players
n138=        if (%ffrbmelement = 3) { write -al4 BlackSpell.txt Element: Time }
n139=        elseif (%ffrbmelement = 4) { write -al4 BlackSpell.txt Element: Death }
n140=        elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n141=        elseif (%ffrbmelement = 0) { write -al4 BlackSpell.txt Element: None }
n142=        else { write -al4 BlackSpell.txt Element: Poison }
n143=        ; Poisons come in Time, Death, and Fire flavours
n144=        ; But mostly Poison
n145=      }
n146=      elseif (%ffrbmdebuff = 2) { 
n147=        write -al3 BlackSpell.txt Effect: Dark
n148=        if (%ffrbmelement = 2) {
n149=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n150=          write -al4 BlackSpell.txt Element: Poison
n151=        }
n152=        elseif (%ffrbmelement = 3) {
n153=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n154=          write -al4 BlackSpell.txt Element: Time
n155=        }
n156=        elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n157=        elseif (%ffrbmelement = 8) {
n158=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n159=          write -al4 BlackSpell.txt Element: Earth
n160=        }
n161=        elseif (%ffrbmelement = 0) { write -al4 BlackSpell.txt Element: None }
n162=        else { write -al4 BlackSpell.txt Element: Status }
n163=        ; Darkness comes in Poison, Time, and Earth flavours
n164=        ; But mostly Status
n165=      }
n166=      elseif (%ffrbmdebuff = 3) {
n167=        write -al3 BlackSpell.txt Effect: Stun
n168=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 3 }
n169=        if (%ffrbmelement = 3) {
n170=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 4 }
n171=          write -al4 BlackSpell.txt Element: Time
n172=        }
n173=        elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n174=        elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n175=        elseif (%ffrbmelement = 0) {
n176=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 5 }
n177=          write -al4 BlackSpell.txt Element: None
n178=        }
n179=        else { write -al4 BlackSpell.txt Element: Status }
n180=        ; Stuns come in Time, Ice, and Lit flavours
n181=        ; But mostly Status
n182=        ; (Change made in May 2019) Black gets an accuracy bonus (redesigned in Dec 2021)
n183=      }
n184=      elseif (%ffrbmdebuff = 4) {
n185=        if (%ffrspellbinding = 1) {
n186=          inc %ffrbmaccuracy 1
n187=          if (%ffrsplevel > 1) { inc %ffrbmaccuracy 2 }
n188=        }
n189=        write -al3 BlackSpell.txt Effect: Sleep
n190=        if (%ffrbmelement = 1) { write -al4 BlackSpell.txt Element: Status }
n191=        elseif (%ffrbmelement = 3) {
n192=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n193=          write -al4 BlackSpell.txt Element: Time
n194=        }
n195=        elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n196=        elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n197=        else { write -al4 BlackSpell.txt Element: None }
n198=        ; Sleep comes in Status, Time, Fire, and Ice flavours
n199=        ; But mostly Non-Elemental
n200=      }
n201=      elseif (%ffrbmdebuff = 5) {
n202=        write -al3 BlackSpell.txt Effect: Mute
n203=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n204=        if (%ffrbmelement = 2) {
n205=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n206=          write -al4 BlackSpell.txt Element: Poison
n207=        }
n208=        elseif (%ffrbmelement = 3) {
n209=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n210=          write -al4 BlackSpell.txt Element: Time
n211=        }
n212=        elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n213=        elseif (%ffrbmelement = 0) { write -al4 BlackSpell.txt Element: None }
n214=        else { write -al4 BlackSpell.txt Element: Status }
n215=        ; Mutes come in Poison, Time, and Ice flavours
n216=        ; But mostly Status
n217=      }
n218=      elseif (%ffrbmdebuff = 6) {
n219=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 4 }
n220=        write -al3 BlackSpell.txt Effect: Confuse
n221=        if (%ffrbmelement = 3) {
n222=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n223=          write -al4 BlackSpell.txt Element: Time
n224=        }
n225=        elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n226=        elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n227=        elseif (%ffrbmelement = 0) { write -al4 BlackSpell.txt Element: None }
n228=        else { write -al4 BlackSpell.txt Element: Status }
n229=        ; Confuse comes in Time, Fire, and Lit flavours
n230=        ; But mostly Status
n231=        ; (Change made in May 2019) Black gets an accuracy bonus
n232=      }
n233=      elseif (%ffrbmdebuff = 7) {
n234=        if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n235=        write -al3 BlackSpell.txt Effect: Dehab
n236=        if (%ffrbmelement = 3) {
n237=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n238=          write -al4 BlackSpell.txt Element: Time
n239=        }
n240=        elseif (%ffrbmelement = 4) {
n241=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n242=          write -al4 BlackSpell.txt Element: Death
n243=        }
n244=        elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n245=        elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n246=        elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n247=        elseif (%ffrbmelement = 8) {
n248=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n249=          write -al4 BlackSpell.txt Element: Earth
n250=        }
n251=        elseif (%ffrbmelement = 0) { write -al4 BlackSpell.txt Element: None }
n252=        else { write -al4 BlackSpell.txt Element: Status }
n253=        ; "Dehab" inflicts everything except Death, Stone, Poison, and Confuse
n254=        ; As such, it doesn't come in Poison flavour
n255=      }
n256=      elseif (%ffrbmdebuff = 8) {
n257=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 4 }
n258=        write -al3 BlackSpell.txt Effect: Slow
n259=        if (%ffrbmelement = 1) { write -al4 BlackSpell.txt Element: Status }
n260=        elseif (%ffrbmelement = 2) {
n261=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n262=          write -al4 BlackSpell.txt Element: Poison
n263=        }
n264=        elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n265=        elseif (%ffrbmelement = 4) {
n266=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n267=          write -al4 BlackSpell.txt Element: Death
n268=        }
n269=        elseif (%ffrbmelement = 0) {
n270=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 3 }
n271=          write -al4 BlackSpell.txt Element: None
n272=        }
n273=        else {
n274=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n275=          write -al4 BlackSpell.txt Element: Time
n276=        }
n277=        ; Slows come in Status, Poison, Ice, and Death flavours
n278=        ; But are usually Time
n279=      }
n280=      elseif (%ffrbmdebuff = 9) {
n281=        if (%ffrspellbinding = 1) {
n282=          dec %ffrbmaccuracy 2
n283=          if (%ffrsplevel > 6) { var %ffrbmstrength $rand(40,64) | set %ffrbmtier 4 }
n284=          else {
n285=            var %ffrbmstrength $floor($calc((%ffrsplevel +1)*5))
n286=            if (%ffrbmstrength < 40) { set %ffrbmtier 3 }
n287=            if (%ffrbmstrength < 30) { set %ffrbmtier 2 }
n288=            if (%ffrbmstrength < 20) { set %ffrbmtier 1 }
n289=          }
n290=        }
n291=        write -al3 BlackSpell.txt Effect: Fear
n292=        if (%ffrbmelement = 4) {
n293=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n294=          var %ffrbmstrength $floor($calc(%ffrbmstrength *1.2))
n295=          write -al4 BlackSpell.txt Element: Death
n296=        }
n297=        elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n298=        elseif (%ffrbmelement = 8) {
n299=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n300=          write -al4 BlackSpell.txt Element: Earth
n301=        }
n302=        elseif (%ffrbmelement = 1) { write -al4 BlackSpell.txt Element: Status }
n303=        else { write -al4 BlackSpell.txt Element: None }
n304=        ; Fears come in Death, Fire, Earth, and Status flavours
n305=        ; But usually aren't elemental
n306=      }
n307=      elseif (%ffrbmdebuff = 10) {
n308=        if (%ffrspellbinding = 1) {
n309=          inc %ffrbmaccuracy 4
n310=          if (%ffrsplevel = 8) { var %ffrbmstrength $rand(160,240) | set %ffrbmtier 4 }
n311=          else { var %ffrbmstrength $floor($calc(%ffrsplevel *20)) }
n312=          if (%ffrbmstrength < 160) { set %ffrbmtier 3 }
n313=          if (%ffrbmstrength < 100) { set %ffrbmtier 2 }
n314=          if (%ffrbmstrength < 60) { set %ffrbmtier 1 }
n315=        }
n316=        write -al3 BlackSpell.txt Effect: Locked
n317=        if (%ffrbmelement = 3) {
n318=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n319=          write -al4 BlackSpell.txt Element: Time
n320=        }
n321=        elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n322=        elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n323=        elseif (%ffrbmelement = 8) {
n324=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n325=          write -al4 BlackSpell.txt Element: Earth
n326=        }
n327=        else { write -al4 BlackSpell.txt Element: None }
n328=        ; Locks come in Time, Ice, Lit, and Earth flavours
n329=        ; But usually aren't elemental
n330=        ; (Change made in May 2019) Black gets an accuracy bonus (redesigned in Dec 2021)
n331=      }
n332=      elseif (%ffrbmdebuff = 11) {
n333=        if (%ffrspellbinding = 1) {
n334=          inc %ffrbmaccuracy 1
n335=          if (%ffrsplevel = 8) { dec %ffrbmaccuracy 1 }
n336=        }
n337=        write -al3 BlackSpell.txt Effect: Dispel
n338=        if (%ffrbmelement = 3) {
n339=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n340=          write -al4 BlackSpell.txt Element: Time
n341=        }
n342=        elseif (%ffrbmelement = 4) {
n343=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n344=          write -al4 BlackSpell.txt Element: Death
n345=        }
n346=        elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n347=        elseif (%ffrbmelement = 1) { write -al4 BlackSpell.txt Element: Status }
n348=        else {
n349=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 2 }
n350=          write -al4 BlackSpell.txt Element: None
n351=        }
n352=        ; Dispel is the Black name for Xfer
n353=        ; (White calls it Stripped)
n354=        ; Curiously, it can be Time, Death, Lit, or Status
n355=        ; It usually doesn't have an element, thankfully
n356=      }
n357=      ; Power Words get different elemental selection
n358=      elseif (%ffrbmdebuff = 12) {
n359=        if (%ffrspellbinding = 1) && (%ffrsplevel < 6) { echo 11 -s Debug: Bounced a Power Word before Level 6 | goto blackmagic }
n360=        var %ffrbmaccskip 1
n361=        if (%ffrbmafflict = 9) {
n362=          write -a13 BlackSpell.txt Effect: Power Word "Debilitate"
n363=          if (%ffrbmelement = 0) && ($rand(1,5) = 5) { write -al4 BlackSpell.txt Element: None }
n364=          elseif (%ffrbmelement = 3) && ($rand(1,4) = 4) { write -al4 BlackSpell.txt Element: Time }
n365=          elseif (%ffrbmelement = 4) { write -al4 BlackSpell.txt Element: Death }
n366=          else { write -al4 BlackSpell.txt Element: Status }
n367=          ; Dehab heavily favours Status now
n368=          ; But it can still be Time or Death
n369=        }
n370=        elseif (%ffrbmafflict = 1) {
n371=          write -al3 BlackSpell.txt Effect: Power Word "Kill"
n372=          if (%ffrbmelement = 2) { write -al4 BlackSpell.txt Element: Poison }
n373=          elseif (%ffrbmelement = 3) && ($rand(1,5) = 5) { write -al4 BlackSpell.txt Element: Time }
n374=          elseif (%ffrbmelement = 8) { write -al4 BlackSpell.txt Element: Earth }
n375=          elseif (%ffrbmelement = 0) && ($rand(1,10) = 10) { write -al4 BlackSpell.txt Element: None }
n376=          else { write -al4 BlackSpell.txt Element: Death }
n377=          ; Usually, "Kill" is RUB
n378=          ; Sometimes it pulls QAKE, BANE, or ZAP! now
n379=        }
n380=        elseif (%ffrbmafflict = 2) {
n381=          write -al3 BlackSpell.txt Effect: Power Word "Break"
n382=          if (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n383=          elseif (%ffrbmelement = 0) && ($rand(1,10) = 10) { write -al4 BlackSpell.txt Element: None }
n384=          else { write -al4 BlackSpell.txt Element: Stone }
n385=          ; Oh, and Power Words can cast BRAK too
n386=          ; As well as an Ice variant
n387=        }
n388=        elseif (%ffrbmafflict = 3) || (%ffrbmafflict = 8) {
n389=          write -al3 BlackSpell.txt Effect: Power Word "Decay"
n390=          if (%ffrbmelement = 3) && ($rand(1,2) = 2) { write -al4 BlackSpell.txt Element: Time }
n391=          elseif (%ffrbmelement = 4) { write -al4 BlackSpell.txt Element: Death }
n392=          elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n393=          elseif (%ffrbmelement = 0) && ($rand(1,4) = 4) { write -al4 BlackSpell.txt Element: None }
n394=          else { write -al4 BlackSpell.txt Element: Poison }
n395=          ; Poison and Confuse are two sides of the same coin
n396=          ; One ticks down players, the other does enemies
n397=          ; So Power Word "Decay" bundles them into one
n398=          ; It can be Time, Death, or even Fire!
n399=          ; Usually just Poison though
n400=        }
n401=        elseif (%ffrbmafflict = 4) {
n402=          write -al3 BlackSpell.txt Effect: Power Word "Blind"
n403=          if (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n404=          elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n405=          elseif (%ffrbmelement = 0) { write -al4 BlackSpell.txt Element: None }
n406=          else { write -al4 BlackSpell.txt Element: Status }
n407=          ; BLND has a fairly poor rep, being Darkness
n408=          ; so the Power Word gets to be Fire or Lit
n409=          ; usually just ends up being Status though
n410=        }
n411=        elseif (%ffrbmafflict = 5) {
n412=          write -al3 BlackSpell.txt Effect: Power Word "Stun"
n413=          if (%ffrbmelement = 3) && ($rand(1,2) = 2) { write -al4 BlackSpell.txt Element: Time }
n414=          elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n415=          elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n416=          elseif (%ffrbmelement = 0) && ($rand(1,4) = 4) { write -al4 BlackSpell.txt Element: None }
n417=          else { write -al4 BlackSpell.txt Element: Status }
n418=          ; STUN also gets privilege like BLND
n419=          ; It can be Ice or Lit now
n420=          ; Still, usually just Status
n421=        }
n422=        elseif (%ffrbmafflict = 6) || (%ffrbmafflict = 7) {
n423=          write -al3 BlackSpell.txt Effect: Power Word "Coma"
n424=          if (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n425=          elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n426=          elseif (%ffrbmelement = 0) { write -al4 BlackSpell.txt Element: None }
n427=          else { write -al4 BlackSpell.txt Element: Status }
n428=          ; Nobody cares about Sleep
n429=          ; Even as a Power Word it's not powerful
n430=          ; So it was bundled in with Mute
n431=          ; It can be Fire or Ice elements
n432=          ; Usually just Status
n433=        }
n434=      }
n435=    }
n436=
n437=    ; Buffs
n438=    ; Black Magic isn't known for being helpful
n439=    ; All Buffs have initial power cut in half
n440=
n441=    ; Process jumps back to here if FAST needs to reroll
n442=    ; so that it doesn't throw away a lot of buffs
n443=    :fastreroll
n444=    if (%ffrbmalter = 2) { 
n445=      var %ffrbmstrength $floor($calc(%ffrbmstrength /2))
n446=      var %ffrbmaccskip 1
n447=      if (%ffrbmbuff = 1) {
n448=        var %ffrbmtarget 2
n449=        write -al3 BlackSpell.txt Effect: Absorb Up
n450=        if (%ffrspellbinding = 1) {
n451=          if (%ffrsplevel = 8) { var %ffrbmstrength 24 | set %ffrbmtier 4 }
n452=          if (%ffrsplevel < 8) { var %ffrbmstrength 16 | set %ffrbmtier 3 }
n453=          if (%ffrsplevel < 5) { var %ffrbmstrength 12 | set %ffrbmtier 2 }
n454=          if (%ffrsplevel < 3) { var %ffrbmstrength 8 | set %ffrbmtier 1 }
n455=        }
n456=        else { var %ffrbmstrength $ceil($calc(%ffrbmstrength * $rand(8,11)/10)) }
n457=      }
n458=      elseif (%ffrbmbuff = 2) {
n459=        var %ffrbmstrskip 1
n460=        if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 2-4)) || ((%ffrspellbinding != 1) && (%ffrbmstrength isnum 10-24)) { var %ffrbmtarget 2 }
n461=        else { var %ffrbmtarget 1 }
n462=        if ((%ffrspellbinding = 1) && (%ffrsplevel = 8)) || ((%ffrspellbinding != 1) && ($rand(1,8) = 8)) {
n463=          if (%ffrspwall != 3) {
n464=            if (%ffrspresist > 4) {
n465=              echo 4 -s Resists already capped! Rerolling.
n466=              goto blackmagic
n467=            }
n468=            set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend all
n469=            set %ffrspwall 3
n470=            inc %ffrspresist 1
n471=          }
n472=          write -al3 BlackSpell.txt Effect: Resist All
n473=          goto barrskip
n474=        }
n475=        if (%ffrbmelement = 1) {
n476=          var %ffrbmresist Status
n477=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) { var %ffrspresmagic 1 }
n478=          else {
n479=            if (%ffrspantiweak != 4) {
n480=              if (%ffrspresist > 4) {
n481=                echo 4 -s Resists already capped! Rerolling.
n482=                goto blackmagic
n483=              }
n484=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend hindrance }
n485=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend weak }
n486=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend psi }
n487=              set %ffrspantiweak 4
n488=              inc %ffrspresist 1
n489=            }
n490=          }
n491=        }
n492=        elseif (%ffrbmelement = 2) {
n493=          var %ffrbmresist Poison/Stone
n494=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) { var %ffrspresdecay 1 }
n495=          else {
n496=            if (%ffrspantibane != 5) {
n497=              if (%ffrspresist > 4) {
n498=                echo 4 -s Resists already capped! Rerolling.
n499=                goto blackmagic
n500=              }
n501=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend poisonous }
n502=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend bane }
n503=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend gas }
n504=              set %ffrspantibane 5
n505=              inc %ffrspresist 1
n506=            }
n507=          }
n508=        }
n509=        elseif (%ffrbmelement = 3) {
n510=          var %ffrbmresist Time
n511=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) { var %ffrspresdecay 1 }
n512=          else {
n513=            if (%ffrspantizap != 5) {
n514=              if (%ffrspresist > 4) {
n515=                echo 4 -s Resists already capped! Rerolling.
n516=                goto blackmagic
n517=              }
n518=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend dimension }
n519=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend time }
n520=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend age }
n521=              set %ffrspantizap 5
n522=              inc %ffrspresist 1
n523=            }
n524=          }
n525=        }
n526=        elseif (%ffrbmelement = 4) {
n527=          var %ffrbmresist Death
n528=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) {
n529=            if ($rand(1,2) = 1) { var %ffrspresmagic 1 }
n530=            else { var %ffrspresdecay 1 }
n531=          }
n532=          else {
n533=            if (%ffrspantinecro != 4) {
n534=              if (%ffrspresist > 4) {
n535=                echo 4 -s Resists already capped! Rerolling.
n536=                goto blackmagic
n537=              }
n538=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend necrotic }
n539=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend evil }
n540=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend rub }
n541=              set %ffrspantinecro 4
n542=              inc %ffrspresist 1
n543=            }
n544=          }
n545=        }
n546=        elseif (%ffrbmelement = 5) {
n547=          var %ffrbmresist Fire
n548=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) { var %ffrspresdragon 1 }
n549=          else {
n550=            if (%ffrspantifire != 4) {
n551=              if (%ffrspresist > 4) {
n552=                echo 4 -s Resists already capped! Rerolling.
n553=                goto blackmagic
n554=              }
n555=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend burning }
n556=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend fire }
n557=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend hot }
n558=              set %ffrspantifire 4
n559=              inc %ffrspresist 1
n560=            }
n561=          }
n562=        }
n563=        elseif (%ffrbmelement = 6) {
n564=          var %ffrbmresist Ice
n565=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) { var %ffrspresdragon 1 }
n566=          else {
n567=            if (%ffrspantiice != 4) {
n568=              if (%ffrspresist > 4) {
n569=                echo 4 -s Resists already capped! Rerolling.
n570=                goto blackmagic
n571=              }
n572=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend freezing }
n573=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend cold }
n574=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend ice }
n575=              set %ffrspantiice 4
n576=              inc %ffrspresist 1
n577=            }
n578=          }
n579=        }
n580=        elseif (%ffrbmelement = 7) {
n581=          var %ffrbmresist Lit
n582=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) { var %ffrspresdragon 1 }
n583=          else {
n584=            if (%ffrspantilightning != 9) {
n585=              if (%ffrspresist > 4) {
n586=                echo 4 -s Resists already capped! Rerolling.
n587=                goto blackmagic
n588=              }
n589=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend lightning }
n590=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend volt }
n591=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend ion }
n592=              set %ffrspantilightning 9
n593=              inc %ffrspresist 1
n594=            }
n595=          }
n596=        }
n597=        elseif (%ffrbmelement = 8) {
n598=          var %ffrbmresist Earth
n599=          if ((%ffrspellbinding = 1) && (%ffrsplevel isnum 5-7)) || ((%ffrspellbinding != 1) && (%ffrbmstrength > 24)) { var %ffrspresmagic 1 }
n600=          else {
n601=            if (%ffrspantiquake != 5) {
n602=              if (%ffrspresist > 4) {
n603=                echo 4 -s Resists already capped! Rerolling.
n604=                goto blackmagic
n605=              }
n606=              elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend tectonic }
n607=              elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend land }
n608=              else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend geo }
n609=              set %ffrspantiquake 5
n610=              inc %ffrspresist 1
n611=            }
n612=          }
n613=        }
n614=        else {
n615=          var %ffrbmstrskip 0
n616=          write -al3 BlackSpell.txt Effect: Regenerate
n617=          if (%ffrspellbinding = 1) { set %ffrbmregmod %ffrsplevel }
n618=          else { set %ffrbmregmod $calc(%ffrbmstrength /6) }
n619=          var %ffrbmstrength $floor($calc(($rand(18,22) /10)^ ($rand(25,40) /10 + %ffrbmregmod / 2) + (%ffrbmregmod - 1) /2 ))
n620=          if (%ffrspellbinding = 1) {
n621=            if (%ffrsplevel < 7) { set %ffrbmtier 3 }
n622=            if (%ffrsplevel < 5) { set %ffrbmtier 2 }
n623=            if (%ffrsplevel < 3) { set %ffrbmtier 1 }
n624=          }
n625=          else { set %ffrbmtier $ceil($calc(%ffrbmstrength / $rand(32,64)))
n626=            if (%ffrbmtier > 3) { echo 7 -s Regen Power: %ffrbmstrength }
n627=          }
n628=        }
n629=        if (%ffrspresmagic = 1) {
n630=          var %ffrbmresist Magic
n631=          if (%ffrspantimagic != 5) {
n632=            if (%ffrspresist > 4) {
n633=              echo 4 -s Resists already capped! Rerolling.
n634=              goto blackmagic
n635=            }
n636=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend mortality }
n637=            elseif (%ffrspresist isnum 1-2) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend life }
n638=            elseif (%ffrspresist = 3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend magic }
n639=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend bio }
n640=            set %ffrspantimagic 5
n641=            inc %ffrspresist 1
n642=          }
n643=        }
n644=        elseif (%ffrspresdecay = 1) {
n645=          var %ffrbmresist Decay
n646=          if (%ffrspantitoxin != 5) {
n647=            if (%ffrspresist > 4) {
n648=              echo 4 -s Resists already capped! Rerolling.
n649=              goto blackmagic
n650=            }
n651=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend futility }
n652=            elseif (%ffrspresist isnum 1-2) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend doom }
n653=            elseif (%ffrspresist = 3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend waste }
n654=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend rot }
n655=            set %ffrspantitoxin 5
n656=            inc %ffrspresist 1
n657=          }
n658=        }
n659=        elseif (%ffrspresdragon = 1) {
n660=          var %ffrbmresist Dragon
n661=          if (%ffrspantidamage != 5) {
n662=            if (%ffrspresist > 4) {
n663=              echo 4 -s Resists already capped! Rerolling.
n664=              goto blackmagic
n665=            }
n666=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend elemental }
n667=            elseif (%ffrspresist isnum 1-2) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend wyrm }
n668=            elseif (%ffrspresist = 3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend spell }
n669=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend wiz }
n670=            set %ffrspantidamage 5
n671=            inc %ffrspresist 1
n672=          }
n673=        }
n674=        if (%ffrbmelement isnum 1-8) { write -al3 BlackSpell.txt Effect: Resist %ffrbmresist }
n675=      }
n676=      elseif (%ffrbmbuff = 3) {
n677=        ; if (%ffrspellbinding != 1) {
n678=        ;   if ($rand(0,1) = 1) { var %ffrbmbuff $rand(1,5) | /echo 11 -s Debug: Rerolling failed FAST (0 vs $v1 $+ ) | goto fastreroll }
n679=        ; }
n680=        ; if ($rand(1,20) > $rand(1,100)) { var %ffrbmtarget 3 | /echo 7 Debug: Passed FURY roll ( $+ $v1 vs $v2 $+ ) }
n681=        if ((%ffrspellbinding != 1) && (%ffrbmstrength < 25)) || ((%ffrspellbinding = 1) && (%ffrsplevel < 4)) { var %ffrbmtarget 1 }
n682=        elseif ((%ffrspellbinding != 1) && (%ffrbmstrength >= 40)) || ((%ffrspellbinding = 1) && (%ffrsplevel > 7)) { var %ffrbmtarget 3 }
n683=        else { var %ffrbmtarget 2 }
n684=        ; Trying out power-based FAST targetting rather than random
n685=        var %ffrbmstrskip 1
n686=        write -al3 BlackSpell.txt Effect: Double Hits
n687=        ; However, Black Magic likes to hurt things
n688=        ; FAST hurts things. A lot.
n689=        ; To prevent most buffs just rolling FAST,
n690=        ; it has to win a coinflip to pass
n691=        ; Also has a 20% chance to boost the whole party
n692=      }
n693=      elseif (%ffrbmbuff = 4) {
n694=        write -al3 BlackSpell.txt Effect: Attack Up 
n695=        if (%ffrbmtarget > 1) {
n696=          if (%ffrspellbinding = 1) {
n697=            var %ffrbmstrength $floor($calc(%ffrsplevel *3+ $rand(3,5)))
n698=            if (%ffrbmstrength < 27) { set %ffrbmtier 3 }
n699=            else { set %ffrbmtier 4 }
n700=            if (%ffrbmstrength < 18) { set %ffrbmtier 2 }
n701=            if (%ffrbmstrength < 12) { set %ffrbmtier 1 }
n702=          }
n703=          else { var %ffrbmstrength $calc($rand(5,19) + $rand(1,10)) }
n704=          if ($rand(1,20) > $rand(1,50)) {
n705=            var %ffrbmtarget 3
n706=            if (%ffrspellbinding = 1) { var %ffrbmstrength $floor($calc(%ffrbmstrength / (($rand(100,120) /10 - %ffrsplevel)/2)+5)) }
n707=            else { var %ffrbmstrength $ceil($calc(%ffrbmstrength * ($rand(75,100)/100))) }
n708=          }
n709=          else { /echo 7 Debug: RALY failed ( $+ $v1 vs $v2 $+ ) }
n710=        }
n711=        ; TMPR hurts things too
n712=        ; It comes in varying strengths so no coinflip
n713=        ; It use its own dice, which end up between
n714=        ; 6 and 29, averaging high teens to low 20s
n715=        else {
n716=          if (%ffrspellbinding = 1) {
n717=            var %ffrbmstrength $floor($calc(2^(1.5+ %ffrsplevel /2) +(9- %ffrsplevel)+ %ffrsplevel /2))
n718=            if (%ffrbmstrength < 50) { set %ffrbmtier 3 }
n719=            else { set %ffrbmtier 4 }
n720=            if (%ffrbmstrength < 22) { set %ffrbmtier 2 }
n721=            if (%ffrbmstrength < 15) { set %ffrbmtier 1 }
n722=          }
n723=          else { var %ffrbmstrength $rand(5,50) }
n724=        }
n725=        ; SABR is allowed to wildly vary from 5 to 50
n726=      }
n727=      elseif (%ffrbmbuff = 5) {
n728=        var %ffrbmtarget 2
n729=        write -al3 BlackSpell.txt Effect: Evade Up
n730=        if (%ffrspellbinding = 1) {
n731=          if (%ffrsplevel = 8) { var %ffrbmstrength 80 | set %ffrbmtier 4 }
n732=          if (%ffrsplevel < 8) { var %ffrbmstrength 60 | set %ffrbmtier 3 }
n733=          if (%ffrsplevel < 5) { var %ffrbmstrength 40 | set %ffrbmtier 2 }
n734=          if (%ffrsplevel < 3) { var %ffrbmstrength 30 | set %ffrbmtier 1 }
n735=        }
n736=        else { var %ffrbmstrength $floor($calc(%ffrbmstrength * $rand(10,18)/10)) }
n737=      }
n738=    }
n739=  }
n740=
n741=  ; Offensive Spells need Elements too
n742=  ; Black Magic is better at damaging
n743=  ; So it never uses Status, Time, or Earth
n744=  ; Those instead give more weight to Fire, Ice, and Lit
n745=  ; It has a bit of a fetish for Poison and Death though
n746=  ; Death hits harder
n747=
n748=  ; Non-elemental Instakills have to pass a 10% check
n749=  ; Fire, Ice, and Lit Instakills have to pass 25%
n750=  ; Smoke=Poison, Break=Stone, Exile=Time, Erase=Death
n751=  ; So Ice inflicts Stone and requires Softening
n752=  if (%ffrbmeffect = 1) {
n753=    if (%ffrbmohko < 4) { 
n754=      :blackohko
n755=      if (%ffrbmelement = 0) {
n756=        if ($rand(1,10) = 10) { write -al4 BlackSpell.txt Element: None }
n757=        else {
n758=          /echo 11 -s Debug: FALL failed ( $+ $v1 vs 10). Switching to QAKE.
n759=          var %ffrbmelement 8
n760=          goto blackohko
n761=        }
n762=      }
n763=      elseif (%ffrbmelement = 1) {
n764=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n765=        write -al4 BlackSpell.txt Element: Smoke
n766=      }
n767=      elseif (%ffrbmelement = 2) {
n768=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 3 }
n769=        write -al4 BlackSpell.txt Element: Break
n770=      }
n771=      elseif (%ffrbmelement = 3) {
n772=        if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n773=        write -al4 BlackSpell.txt Element: Exile
n774=      }
n775=      elseif (%ffrbmelement = 4) {
n776=        if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n777=        write -al4 BlackSpell.txt Element: Erase
n778=      }
n779=      elseif (%ffrbmelement = 5) {
n780=        if ($rand(1,4) = 4) { write -al4 BlackSpell.txt Element: Fire }
n781=        ; Use "Erased"
n782=        else { /echo 11 -s Debug: MELT failed ( $+ $v1 vs 4). Switching to RUB.
n783=          var %ffrbmelement 4
n784=          goto blackohko
n785=        }
n786=      }
n787=      elseif (%ffrbmelement = 6) {
n788=        if ($rand(1,4) = 4) {
n789=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 1 }
n790=          write -al4 BlackSpell.txt Element: Ice
n791=        }
n792=        ; Use "Broken"
n793=        else { /echo 11 -s Debug: CRYO failed ( $+ $v1 vs 4). Switching to a poison.
n794=          var %ffrbmelement $rand(1,2)
n795=          goto blackohko
n796=        }
n797=      }
n798=      elseif (%ffrbmelement = 7) {
n799=        if ($rand(1,4) = 4) { write -al4 BlackSpell.txt Element: Lit }
n800=        ; Use "Exiled"
n801=        else { /echo 11 -s Debug: SMIT failed ( $+ $v1 vs 4). Switching to ZAP!
n802=          var %ffrbmelement 3
n803=          goto blackohko
n804=        }
n805=      }
n806=      elseif (%ffrbmelement = 8) {
n807=        if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n808=        write -al4 BlackSpell.txt Element: Crack
n809=      }
n810=    }
n811=    else {
n812=      if ($rand(1,3) = 3) { echo 11 -s Debug: Forcing element to Fire, Ice, or Lit ( $+ $v1 vs 3) | var %ffrbmelement $rand(5,7) }
n813=      :blackdamage
n814=      if (%ffrbmelement = 1) {
n815=        if ($rand(1,20) > $rand(1,60)) {
n816=          var %ffrbmstrength $floor($calc(%ffrbmstrength *1.25))
n817=          write -al4 BlackSpell.txt Element: Kinetic
n818=        }
n819=        else {
n820=          write -al4 BlackSpell.txt Element: Lit
n821=          echo 11 -s Debug: Kinetic failed ( $+ $v1 vs $v2 $+ )
n822=        }
n823=      }
n824=      elseif (%ffrbmelement = 2) { 
n825=        if ($rand(1,20) > $rand(1,60)) { 
n826=          var %ffrbmstrength $floor($calc(%ffrbmstrength *1.1))
n827=          write -al4 BlackSpell.txt Element: Storm
n828=        }
n829=        else {
n830=          echo 11 -s Debug: Storm failed ( $+ $v1 vs $v2 $+ )
n831=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 2 }
n832=          write -al4 BlackSpell.txt Element: Poison
n833=        }
n834=      }
n835=      elseif (%ffrbmelement = 3) {
n836=        if ($rand(1,20) > $rand(1,60)) {
n837=          var %ffrbmstrength $floor($calc(%ffrbmstrength *1.1))
n838=          write -al4 BlackSpell.txt Element: Plasma
n839=        }
n840=        else {
n841=          write -al4 BlackSpell.txt Element: Ice
n842=          echo 11 -s Debug: Plasma failed ( $+ $v1 vs $v2 $+ )
n843=        }
n844=      }
n845=      elseif (%ffrbmelement = 4) {
n846=        if ($rand(1,20) > $rand(1,60)) { 
n847=          var %ffrbmstrength $floor($calc(%ffrbmstrength *1.25))
n848=          write -al4 BlackSpell.txt Element: Antipode
n849=        }
n850=        else {
n851=          echo 11 -s Debug: Antipode failed ( $+ $v1 vs $v2 $+ )
n852=          if (%ffrspellbinding = 1) { dec %ffrbmaccuracy 1 }
n853=          var %ffrbmstrength $floor($calc( %ffrbmstrength * 1.2 ))
n854=          if (%ffrbmstrength = 144) { dec %ffrbmstrength 24 }
n855=          write -al4 BlackSpell.txt Element: Death
n856=        }
n857=      }
n858=      elseif (%ffrbmelement = 5) { write -al4 BlackSpell.txt Element: Fire }
n859=      elseif (%ffrbmelement = 6) { write -al4 BlackSpell.txt Element: Ice }
n860=      elseif (%ffrbmelement = 7) { write -al4 BlackSpell.txt Element: Lit }
n861=      elseif (%ffrbmelement = 8) {
n862=        if ($rand(1,20) > $rand(1,60)) {
n863=          var %ffrbmstrength $floor($calc(%ffrbmstrength *1.25))
n864=          write -al4 BlackSpell.txt Element: Magma
n865=        }
n866=        else {
n867=          echo 11 -s Debug: Magma failed ( $+ $v1 vs $v2 $+ )
n868=          write -al4 BlackSpell.txt Element: Fire
n869=        }
n870=      }
n871=      else {
n872=        if ($rand(1,20) >= $rand(1,20)) { 
n873=          write -al4 BlackSpell.txt Element: None
n874=          if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 1 }
n875=        }
n876=        else { /echo 11 -s Debug: NUKE failed ( $+ $v1 vs $v2 $+ ). Switching to basic element.
n877=          var %ffrbmelement $rand(5,7)
n878=          goto blackdamage
n879=        }
n880=      }
n881=    }
n882=  }
n883=
n884=  ; -b2: Ensuring targeting remains
n885=
n886=  if (%ffrbmb2 = 1) {
n887=    if (%ffrsplevel isnum 1-2) { var %ffrbmtarget 1 }
n888=    elseif (%ffrsplevel isnum 3-7) { var %ffrbmtarget 2 }
n889=  }
n890=
n891=  ; Writes the target down to a file
n892=  ; First checks if FAST or TMPR is AoE
n893=  ; Then checks if it's a Buff or not
n894=  ; Only TMPR and FAST can target all allies
n895=  :barrskip
n896=  if (%ffrbmeffect = 2) && (%ffrbmtarget = 3) { write -al5 BlackSpell.txt Target: All Allies | goto targetjump }
n897=  if (%ffrbmeffect = 1) || ((%ffrbmeffect = 2) && (%ffrbmalter = 1)) {
n898=    if (%ffrbmtarget < 2) {
n899=      if (%ffrspellbinding = 1) { inc %ffrbmaccuracy 1 }
n900=      write -al5 BlackSpell.txt Target: Single Enemy
n901=    }
n902=    elseif (%ffrbmtarget > 1) { write -al5 BlackSpell.txt Target: Enemy Party }
n903=  }
n904=  elseif (%ffrbmeffect = 2) && (%ffrbmalter = 2) {
n905=    if (%ffrbmtarget = 1) { write -al5 BlackSpell.txt Target: Caster }
n906=    elseif (%ffrbmtarget = 2) { write -al5 BlackSpell.txt Target: Single Ally }
n907=  }
n908=  ; This just makes sure it doesn't overwrite All Allies
n909=  :targetjump
n910=
n911=  ; Accuracy Balance
n912=  ; Defines the appropriate tiers of Accuracy for Spellbooks
n913=  ; Accuracy no longer caps
n914=  ; Instakills can't be Auto-Hit
n915=  if (%ffrspellbinding = 1) && (%ffrbmaccsay = Undefined) {
n916=    if (%ffrbmaccuracy < 2) { var %ffrbmaccsay 0 }
n917=    elseif (%ffrbmaccuracy = 2) { var %ffrbmaccsay 5 }
n918=    elseif (%ffrbmaccuracy = 3) { var %ffrbmaccsay 8 }
n919=    elseif (%ffrbmaccuracy = 4) { var %ffrbmaccsay 16 }
n920=    elseif (%ffrbmaccuracy = 5) { var %ffrbmaccsay 24 }
n921=    elseif (%ffrbmaccuracy = 6) { var %ffrbmaccsay 32 }
n922=    elseif (%ffrbmaccuracy = 7) { var %ffrbmaccsay 40 }
n923=    elseif (%ffrbmaccuracy = 8) { var %ffrbmaccsay 48 }
n924=    elseif (%ffrbmaccuracy = 9) { var %ffrbmaccsay 64 }
n925=    elseif (%ffrbmaccuracy = 10) { var %ffrbmaccsay 107 }
n926=    elseif (%ffrbmaccuracy = 11) { var %ffrbmaccsay 128 }
n927=    elseif (%ffrbmaccuracy = 12) { var %ffrbmaccsay 152 }
n928=    elseif (%ffrbmaccuracy = 13) { var %ffrbmaccsay 175 }
n929=    elseif (%ffrbmaccuracy = 14) { var %ffrbmaccsay 210 }
n930=    else { var %ffrbmaccsay 255 | echo 7 -s Debug: Assumed Accuracy was Auto-Hit }
n931=  }
n932=
n933=  ; Cleanup Section
n934=
n935=  ; Outputs only information relevant to the spell
n936=  if (%ffrbmstrskip = 0) && (%ffrbmaccskip = 0) { write -s15 BlackSpell.txt Power: %ffrbmstrength | write -al6 BlackSpell.txt Acc Bonus: %ffrbmaccsay }
n937=  elseif (%ffrbmstrskip = 0) && (%ffrbmaccskip = 1) { write -s15 BlackSpell.txt Power: %ffrbmstrength }
n938=  elseif (%ffrbmstrskip = 1) && (%ffrbmaccskip = 0) { write -s15 BlackSpell.txt Acc Bonus: %ffrbmaccsay }
n939=
n940=  ; Rerolls if spell is incompatible during Spellbinding
n941=  if (e isincs %ffrspflags) && (%ffrspreroll < 5) {
n942=    if (%ffrbmeffect = 2) && (%ffrbmalter = 1) && ((%ffrbmdebuff = 6) || (%ffrbmdebuff = 9)) {
n943=      if (%ffrbmdebuff = 6) { var %ffrbmdebug CONF }
n944=      elseif (%ffrbmdebuff = 9) { var %ffrbmdebug FEAR }
n945=      if (%ffrsplevel = 1) || (%ffrsplevel = 5) || (%ffrsplevel = 8) { /echo 11 -s Debug: Bounced %ffrbmdebug during spellbinding | goto blackmagic }
n946=      elseif ((%ffrsplevel = 4) || (%ffrsplevel = 6) || (%ffrsplevel = 7)) && (%ffrspslot != 3) { /echo 11 -s Debug: Bounced %ffrbmdebug during spellbinding | goto blackmagic }
n947=      elseif (%ffrsplevel = 2) && ((%ffrspslot = 2) || (%ffrspslot = 4)) { /echo 11 -s Debug: Bounced %ffrbmdebug during spellbinding | goto blackmagic }
n948=      elseif (%ffrsplevel = 3) && (%ffrspslot != 4) { /echo 11 -s Debug: Bounced %ffrbmdebug during spellbinding | goto blackmagic }
n949=    }
n950=  }
n951=
n952=  ; Skips text output for Spellbinding
n953=  ; That would open 64 Notepad windows
n954=  ; Also keeps track of AoE Damage Spells
n955=  if (%ffrnowrite != 1) { /run BlackSpell.txt }
n956=  elseif (%ffrbmeffect = 1) && (%ffrbmtarget > 1) && (%ffrbmohko > 3) { inc %ffrspblackAoE 1 }
n957=
n958=}
