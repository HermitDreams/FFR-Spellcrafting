[aliases]
n0=/wmag { ; Final Fantasy Randomizer White Magic Generator. IRC Script by Linkshot
n1=  ; White Spell Initiation
n2=  :whitemagic
n3=  write -c WhiteSpell.txt
n4=  var %ffrwmstrskip 0
n5=  var %ffrwmaccskip 0
n6=  var %ffrwmeffect $rand(1,5)
n7=  var %ffrwmdebuff $rand(1,10)
n8=  var %ffrwmheal $rand(1,10)
n9=  var %ffrwmbuff $rand(0,14)
n10=  var %ffrwmelement $rand(0,4)
n11=  var %ffrwmallies $rand(1,3)
n12=  var %ffrwmenemies $rand(1,2)
n13=  var %ffrwmaccroll $rand(0,255)
n14=  var %ffrwmaccuracy $rand($calc((2^$rand(5,6))-1),$calc((2^$rand(1,8))-1))
n15=  var %ffrwmstrength $rand(10,100)
n16=  var %ffrwmafflict $rand(0,8)
n17=  if (L isincs %ffrspflags) { 
n18=    write -i SpellLog.txt Generating White Magic for %ffrsplevel $+ - $+ %ffrspslot
n19=    write -i SpellLog.txt -
n20=    write -i SpellLog.txt Strength Skip and Acc Skip flags reset
n21=    write -i SpellLog.txt Dice Roll for Spell Type: %ffrwmeffect (1-5)
n22=    write -i SpellLog.txt Dice Roll for Debuff Effect: %ffrwmdebuff (1-10)
n23=    write -i SpellLog.txt Dice Roll for Healing: %ffrwmheal (1-10)
n24=    write -i SpellLog.txt Dice Roll for Buffs: %ffrwmbuff (0-14)
n25=    write -i SpellLog.txt Dice Roll for Spell Element: %ffrwmelement (0-4)
n26=    write -i SpellLog.txt Dice Roll for Party Targeting: %ffrwmallies (1-3)
n27=    write -i SpellLog.txt Coin Flip for Enemy Targeting: %ffrwmenemies
n28=    write -i SpellLog.txt Dice Roll for Special Accuracy: %ffrwmaccroll (0-255)
n29=    write -i SpellLog.txt Dice Roll for Hit Bonus: %ffrwmaccuracy (0-255, weighted toward lower numbers)
n30=    write -i SpellLog.txt Dice Roll for Strength: %ffrwmstrength (10-100)
n31=    write -i SpellLog.txt Dice Roll for Affliction: %ffrwmafflict (0-8)
n32=    write -i SpellLog.txt -
n33=  }
n34=  :wmbase
n35=
n36=  ; Sets Accuracy to Auto-Hit or Tiered
n37=  if (%ffrwmaccroll < 2) || (%ffrwmaccroll = 148) { 
n38=    var %ffrwmaccsay Auto-Hit
n39=    if (L isincs %ffrspflags) { write -i SpellLog.txt Accuracy Change: 3/256 succeeded. Spell is slated to get 255 Hit Bonus. | write -i SpellLog.txt - }
n40=  }
n41=  elseif (%ffrspellbinding = 1) {
n42=    if (%ffrsplevel < 5) { var %ffrwmaccuracy 5 | set %ffrwmtier 2 }
n43=    if (%ffrsplevel > 4) { var %ffrwmaccuracy 7 | set %ffrwmtier 3 }
n44=    if (%ffrsplevel = 1) { var %ffrwmaccuracy 4 | set %ffrwmtier 1 }
n45=    if (%ffrsplevel = 8) { var %ffrwmaccuracy 9 | set %ffrwmtier 4 }
n46=    if (L isincs %ffrspflags) { write -i SpellLog.txt Accuracy Change: Spellbinder has set the Accuracy Tier to %ffrwmaccuracy to match Level %ffrsplevel | write -i SpellLog.txt - }
n47=  }
n48=  else {
n49=    var %ffrwmaccsay %ffrwmaccuracy
n50=    if (L isincs %ffrspflags) { write -i SpellLog.txt Accuracy Output has been set to %ffrwmaccsay | write -i SpellLog.txt - }
n51=  }
n52=
n53=  ; Spellbinding Slot Permissions
n54=
n55=  ; Compatibility with HEL2 fix
n56=  if (S isincs %ffrspflags) && (%ffrsplevel = 5) && (%ffrspslot = 4) && (%ffrspreroll < 5) {
n57=    var %ffrwmeffect 4
n58=    var %ffrwmheal 1
n59=    var %ffrwmallies 3
n60=    if (L isincs %ffrspflags) {
n61=      write -i SpellLog.txt Flag "S", Level 5, and Slot 4 detected. Reroll counter is at %ffrspreroll of 5.
n62=      write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n63=      write -i SpellLog.txt Healing has been set to 1 (HP Restore)
n64=      write -i SpellLog.txt Party Targeting has been set to 3 (All Allies)
n65=      write -i SpellLog.txt -
n66=    }
n67=  }
n68=
n69=  ; Makes sure Healing lands in 11 specific slots
n70=  if (h isincs %ffrspflags) && (%ffrspreroll < 5) {
n71=    if (L isincs %ffrspflags) { write -i SpellLog.txt Flag "h" is evaluating this spell. Reroll counter is at %ffrspreroll of 5. }
n72=    if (%ffrwmafflict = 8) {
n73=      var %ffrwmafflict 3
n74=      if (L isincs %ffrspflags) { write -i SpellLog.txt Affliction Change: 8 (Confusion) was changed to 3 (Poison) to account for Pure }  
n75=    }
n76=    if (%ffrwmafflict = 5) {
n77=      var %ffrwmafflict 3
n78=      if (L isincs %ffrspflags) { write -i SpellLog.txt AFfliction change: 5 (Stun) was changed to 3 (Poison) to account for Pure }
n79=    }
n80=    if (%ffrsplevel = 1) && (%ffrspslot = 1) {
n81=      var %ffrwmeffect 4
n82=      var %ffrwmallies 2
n83=      var %ffrwmheal 1
n84=      if (L isincs %ffrspflags) {
n85=        write -i SpellLog.txt Level 1 and Slot 1 detected
n86=        write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n87=        write -i SpellLog.txt Party Targeting has been set to 2 (Single Ally)
n88=        write -i SpellLog.txt Healing has been set to 1 (HP Restore)
n89=      }
n90=    }
n91=    elseif (%ffrsplevel = 3) && (%ffrspslot !isnum 2-3) {
n92=      var %ffrwmeffect 4
n93=      var %ffrwmheal 1
n94=      if (L isincs %ffrspflags) {
n95=        write -i SpellLog.txt Level 3 detected outside of Slots 2 and 3
n96=        write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n97=        write -i SpellLog.txt Healing has been set to 1 (HP Restore)
n98=      }
n99=      if (%ffrspslot = 1) {
n100=        var %ffrwmallies 2
n101=        if (L isincs %ffrspflags) { write -i SpellLog.txt Slot 1 confirmed. Party Targeting has been set to 2 (Single Ally) }
n102=      }
n103=      elseif (%ffrspslot = 4) {
n104=        var %ffrwmallies 3
n105=        if (L isincs %ffrspflags) { write -i SpellLog.txt Slot 4 confirmed. Party Targeting has been set to 3 (Whole Party) }
n106=      }
n107=    }
n108=    elseif (%ffrsplevel = 4) && (%ffrspslot = 1) {
n109=      var %ffrwmeffect 4
n110=      var %ffrwmallies 2
n111=      var %ffrwmheal 7
n112=      var %ffrwmafflict 3
n113=      if (L isincs %ffrspflags) {
n114=        write -i SpellLog.txt Level 4 and Slot 1 detected
n115=        write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n116=        write -i SpellLog.txt Party Targeting has been set to 2 (Single Ally)
n117=        write -i SpellLog.txt Healing has been set to 7 (Remove Ailment)
n118=        write -i SpellLog.txt AFfliction has been set to 3 (Poison)
n119=      }
n120=    }
n121=    elseif (%ffrsplevel = 5) && (%ffrspslot != 3) {
n122=      var %ffrwmeffect 4
n123=      if (L isincs %ffrspflags) {
n124=        write -i SpellLog.txt Level 5 outside Slot 3 has been detected
n125=        write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n126=      }
n127=      if (%ffrspslot = 2) {
n128=        var %ffrwmallies 2
n129=        var %ffrwmheal 7
n130=        var %ffrwmafflict 1
n131=        if (L isincs %ffrspflags) {
n132=          write -i SpellLog.txt Slot 2 confirmed
n133=          write -i SpellLog.txt Party Targeting has been set to 2 (Single Ally)
n134=          write -i SpellLog.txt Healing has been set to 7 (Remove Ailment)
n135=          write -i SpellLog.txt Affliction has been set to 1 (Death)
n136=        }
n137=      }
n138=      else {
n139=        var %ffrwmheal 1
n140=        if (L isincs %ffrspflags) { write -i SpellLog.txt Slot 2 deconfirmed. Healing set to 1 (HP Restore) }
n141=        if (%ffrspslot = 1) {
n142=          var %ffrwmallies 2
n143=          if (L isincs %ffrspflags) { write -i SpellLog.txt Slot 1 confirmed. Party Targeting has been set to 2 (Single Ally) }
n144=        }
n145=        elseif (%ffrspslot = 4) {
n146=          var %ffrwmallies 3
n147=          if (L isincs %ffrspflags) { write -i SpellLog.txt Slot 4 confirmed. Party Targeting has been set to 3 (Whole Party) }
n148=        }
n149=      }
n150=    }
n151=    elseif (%ffrsplevel = 6) && (%ffrspslot = 1) {
n152=      var %ffrwmeffect 4
n153=      var %ffrwmallies 2
n154=      var %ffrwmheal 7
n155=      var %ffrwmafflict 2
n156=      if (L isincs %ffrspflags) {
n157=        write -i SpellLog.txt Level 6 and Slot 1 detected
n158=        write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n159=        write -i SpellLog.txt Party Targeting has been set to 2 (Single Ally)
n160=        write -i SpellLog.txt Healing has been set to 7 (Remove Ailment)
n161=        write -i SpellLog.txt Affliction has been set to 2 (Stone)
n162=      }
n163=    }
n164=    elseif (%ffrsplevel = 7) && (%ffrspslot !isnum 2-3) {
n165=      var %ffrwmeffect 4
n166=      if (L isincs %ffrspflags) {
n167=        write -i SpellLog.txt Level 7 outside Slots 2 and 3 detected
n168=        write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n169=      }
n170=      if (%ffrspslot = 1) {
n171=        var %ffrwmallies 2
n172=        var %ffrwmheal 10
n173=        if (L isincs %ffrspflags) {
n174=          write -i SpellLog.txt Slot 1 confirmed
n175=          write -i SpellLog.txt Party Targeting has been set to 2 (Single Ally)
n176=          write -i SpellLog.txt Healing has been set to 10 (HP Max)
n177=        }
n178=      }
n179=      elseif (%ffrspslot = 4) {
n180=        var %ffrwmallies 3
n181=        var %ffrwmheal 1
n182=        if (L isincs %ffrspflags) {
n183=          write -i SpellLog.txt Slot 4 confirmed
n184=          write -i SpellLog.txt Party Targeting has been set to 3 (Whole Party)
n185=          write -i SpellLog.txt Healing has been set to 1 (HP Restore)
n186=        }
n187=      }
n188=    }
n189=    elseif (%ffrsplevel = 8) && (%ffrspslot = 1) {
n190=      var %ffrwmeffect 4
n191=      var %ffrwmallies 3
n192=      var %ffrwmheal 7
n193=      var %ffrwmafflict 1
n194=      if (L isincs %ffrspflags) {
n195=        write -i SpellLog.txt Level 8 and Slot 1 detected
n196=        write -i SpellLog.txt Spell Type has been set to 4 (Curative)
n197=        write -i SpellLog.txt Party Targeting has been set to 3 (Whole Party)
n198=        write -i SpellLog.txt Healing set to 7 (Remove Ailment)
n199=        write -i SpellLog.txt Affliction set to 1 (Death)
n200=        write -i SpellLog.txt *** NOTE: Party Target change will not actually target whole party. LIFE will be changed to LIF2 using this.
n201=      }
n202=    }
n203=    else {
n204=      if (%ffrwmeffect = 4) && ((%ffrwmheal !isnum 7-9) || ((%ffrwmheal isnum 7-9) && (%ffrwmafflict isnum 1-2)) || ((%ffrsplevel < 5) && (%ffrwmheal isnum 7-9) && (%ffrwmafflict = 3))) {
n205=        /echo 13 -s Debug: Bounced healing type %ffrwmheal $+ - $+ %ffrwmafflict at slot %ffrsplevel $+ - $+ %ffrspslot
n206=        if (L isincs %ffrspflags) {
n207=          write -i SpellLog.txt Spell Type 4, Healing %ffrwmheal $+ , and Affliction %ffrwmafflict detected at Level %ffrsplevel
n208=          write -i SpellLog.txt Invalid Curative Spell for these settings. Rerolling White Magic.
n209=          write -i SpellLog.txt A debug message has been sent to the console.
n210=          write -i SpellLog.txt =
n211=        }
n212=        goto whitemagic
n213=      }
n214=    }
n215=    if (L isincs %ffrspflags) { write -i SpellLog.txt Flag "h" has finished evaluating this spell. | write -i SpellLog.txt - }
n216=  }
n217=
n218=  ; Prevents Harm, Confusion, Fear, Soft, and Life
n219=  ; from landing in slots enemies use
n220=  if (e isincs %ffrspflags) && (%ffrspreroll < 5) {
n221=    if (L isincs %ffrspflags) { write -i SpellLog.txt Flag "e" is evaluating this spell. Reroll counter is at %ffrspreroll of 5 }
n222=    if (%ffrwmeffect = 2) || ((%ffrwmeffect = 3) && ((%ffrwmdebuff = 5) || (%ffrwmdebuff = 7) || ((%ffrwmdebuff = 10) && (%ffrwmafflict = 8)))) || ((%ffrwmeffect = 4) && (%ffrwmheal isnum 7-9) && (%ffrwmafflict < 3)) {
n223=      if ((%ffrsplevel = 1) && (%ffrspslot !isnum 2-3)) || ((%ffrsplevel = 2) && (%ffrspslot = 2)) || ((%ffrsplevel = 6) && (%ffrspslot > 2)) || ((%ffrsplevel = 3) && (%ffrspslot > 2)) || (((%ffrsplevel = 5) || (%ffrsplevel = 7)) && ((%ffrspslot = 1) || (%ffrspslot = 4))) || ((%ffrsplevel = 8) && (%ffrspslot > 2)) { 
n224=        if (%ffrwmeffect = 3) { /echo 13 -s Debug: Bounced Debuff Type %ffrwmdebuff at %ffrsplevel $+ - $+ %ffrspslot }
n225=        elseif (%ffrwmeffect = 4) { /echo 13 -s Debug: Bounced Cure Type %ffrwmafflict at %ffrsplevel $+ - $+ %ffrspslot }
n226=        else { /echo 13 -s Debug: Bounced Harm Undead at %ffrsplevel $+ - $+ %ffrspslot }
n227=        if (L isincs %ffrspflags) {
n228=          if (%ffrwmeffect = 2) { write -i SpellLog.txt Spell Type 2 (Harm Undead) detected }
n229=          elseif (%ffrwmeffect = 3) {
n230=            write -i SpellLog.txt Spell Type 3 (Inflict Ailment) detected
n231=            if (%ffrwmdebuff = 5) { write -i SpellLog.txt Debuff 5 (Confusion) confirmed }
n232=            elseif (%ffrwmdebuff = 7) { write -i SpellLog.txt Debuff 7 (Lower Morale) confirmed }
n233=            elseif (%ffrwmdebuff = 10) && (%ffrwmafflict = 8) { write -i SpellLog.txt Debuff 10 (Power Word) detected. AFfliction 8 (Confusion) confirmed }
n234=            elseif (%ffrwmeffect = 4) && (%ffrwmheal isnum 7-9) {
n235=              write -i SpellLog.txt Spell Type 4 (Healing) and Healing %ffrwmheal (Remove Ailment) detected
n236=              if (%ffrwmafflict = 1) { write -i SpellLog.txt AFfliction 1 (Death) confirmed }
n237=              elseif (%ffrwmafflict = 2) { write =i SpellLog.txt AFfliction 2 (Stone) confirmed }
n238=            }              
n239=            write -i SpellLog.txt Enemies use Slot %ffrspslot at Level %ffrsplevel $+ , so the spell has been rejected. Rerolling White Magic.
n240=            write -i SpellLog.txt A debug message has been sent to the console.
n241=            write -i SpellLog.txt =
n242=          }
n243=        }
n244=        goto whitemagic
n245=      }
n246=    }
n247=    if (L isincs %ffrspflags) { write -i SpellLog.txt Flag "e" has finished evaluating this spell. | write -i SpellLog.txt - }
n248=  }
n249=
n250=
n251=  ; Spell-building
n252=
n253=  ; Checks for Raw Damage and forces it to pass
n254=  ; a 10% check, then writes spell type to file
n255=  ; There are only 3 white debuffs in the original spell list
n256=  ; Out of around 30 spells, that's 10% of them
n257=  ; So only half of the debuffs pass the lot check
n258=  if (%ffrwmeffect = 1) {
n259=    if (%ffrspellbinding = 1) {
n260=      if (%ffrsplevel = 2) { dec %ffrwmaccuracy 1 }
n261=      if (%ffrsplevel = 8) {
n262=        var %ffrwmstrength 100
n263=        if (%ffrwmenemies = 1) { inc %ffrwmstrength 20 }
n264=      }
n265=      else { var %ffrwmstrength $calc( %ffrsplevel * 10) }
n266=    }
n267=    else { if (%ffrwmenemies = 1) { var %ffrwmstrength $calc( %ffrwmstrength * ($calc( $rand(100,150) /100))) } }
n268=    var %ffrwmstrength $floor($calc( %ffrwmstrength * 0.8 ))
n269=    if (%ffrwmstrength = 96) { inc %ffrwmstrength 4 }
n270=    if ($rand(1,10) = 10) { write -al2 WhiteSpell.txt Type: Damage
n271=      if (%ffrwmstrength isnum 24-39) { set %ffrwmtier 2 }
n272=      elseif (%ffrwmstrength isnum 40-79) { set %ffrwmtier 3 }
n273=      elseif (%ffrwmstrength >= 80) { set %ffrwmtier 4 }
n274=      else { set %ffrwmtier 1 }
n275=    }
n276=    else { /echo 13 -s Debug: Bounced at Damage ( $+ $v1 vs 10) | goto whitemagic }
n277=  }
n278=  elseif (%ffrwmeffect = 2) { write -al2 WhiteSpell.txt Type: Harm Undead
n279=    if (%ffrspellbinding = 1) {
n280=      inc %ffrwmaccuracy 1
n281=      if (%ffrsplevel = 8) {
n282=        var %ffrwmstrength 100
n283=        if (%ffrwmenemies = 1) { inc %ffrwmstrength 20 }
n284=      }
n285=      else { var %ffrwmstrength $calc( (%ffrsplevel + 1) * 10) }
n286=    }
n287=    if (%ffrwmstrength > 30) { set %ffrwmtier 2 }
n288=    else { set %ffrwmtier 1 }
n289=    if (%ffrwmstrength > 50) { set %ffrwmtier 3 }
n290=    if (%ffrwmstrength > 70) { set %ffrwmtier 4 }
n291=  }
n292=  elseif (%ffrwmeffect = 3) {
n293=    if ($rand(0,1) = 0) {
n294=      inc %ffrwmeffect $rand(1,2)
n295=      if (%ffrwmeffect = 4) { echo 13 -s Debug: Shifting Debuff to Heal }
n296=      elseif (%ffrwmeffect = 5) { echo 13 -s Debug: Shifting Debuff to Buff }
n297=      goto wmbase
n298=    }
n299=    else { echo 8 -s Debug: Debuff passed saving roll ( $+ $v1 vs 0) }
n300=    write -al2 WhiteSpell.txt Type: Debuff
n301=  }
n302=  elseif (%ffrwmeffect = 4) { write -al2 WhiteSpell.txt Type: Heal | echo 8 -s Heal Type: %ffrwmheal & %ffrwmafflict | var %ffrwmaccskip 1 }
n303=  elseif (%ffrwmeffect = 5) { write -al2 WhiteSpell.txt Type: Buff | var %ffrwmaccskip 1 }
n304=  else { /echo 4 -s Value %ffrwmeffect out of range for Base Effect | /halt }
n305=
n306=  ; Non-Damage Spells
n307=
n308=  ; Debuff Selection
n309=  ; White gets an accuracy buff to MUTE, FEAR, and XFER families
n310=  if (%ffrwmeffect = 3) {
n311=    if (%ffrspellbinding = 1) && (%ffrwmaccsay = Auto-Hit) && (%ffrwmdebuff < 6) { var %ffrwmdebuff 10 }
n312=    ; Debug: echo 8 -s Debug: Passing through Debuff (Debuff: %ffrwmdebuff $+ , Element: %ffrwmelement $+ , Affliction: %ffrwmafflict $+ , Allies: %ffrwmallies $+ )
n313=    if (%ffrwmdebuff !isnum 7-8) { var %ffrwmstrskip 1 }
n314=    if (%ffrwmdebuff = 1) { write -al3 WhiteSpell.txt Effect: Darkness } 
n315=    elseif (%ffrwmdebuff = 2) {
n316=      if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 1 }
n317=      if (%ffrwmelement = 0) { dec %ffrwmaccuracy 5 }
n318=      elseif (%ffrwmelement = 3) { dec %ffrwmaccuracy 3 }
n319=      write -al3 WhiteSpell.txt Effect: Stun
n320=    } 
n321=    elseif (%ffrwmdebuff = 3) {
n322=      if (%ffrspellbinding = 1) { dec %ffrwmaccuracy 1 }
n323=      if (%ffrsplevel > 1) { inc %ffrwmaccuracy 2 }
n324=      write -al3 WhiteSpell.txt Effect: Sleep
n325=    }
n326=    elseif (%ffrwmdebuff = 4) {
n327=      write -al3 WhiteSpell.txt Effect: Mute
n328=      if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 4 }
n329=    }
n330=    elseif (%ffrwmdebuff = 5) {
n331=      if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 2 }
n332=      write -al3 WhiteSpell.txt Effect: Confuse
n333=    }
n334=    elseif (%ffrwmdebuff = 6) {
n335=      if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 2 }
n336=      if (%ffrwmelement = 0) { dec %ffrwmaccuracy 3 }
n337=      write -al3 WhiteSpell.txt Effect: Slow
n338=    }
n339=    elseif (%ffrwmdebuff = 7) {
n340=      if (%ffrspellbinding = 1) {
n341=        if (%ffrsplevel > 6) { var %ffrwmstrength $floor($calc(40+ $rand(0,3) *8)) | set %ffrwmtier 4 }
n342=        else {
n343=          var %ffrwmstrength $floor($calc((%ffrsplevel +1)*5))
n344=          if (%ffrwmstrength < 40) { set %ffrwmtier 3 }
n345=          if (%ffrwmstrength < 30) { set %ffrwmtier 2 }
n346=          if (%ffrwmstrength < 20) { set %ffrwmtier 1 }
n347=        }
n348=      }
n349=      write -al3 WhiteSpell.txt Effect: Fear
n350=    }
n351=    elseif (%ffrwmdebuff = 8) {
n352=      if (%ffrspellbinding = 1) {
n353=        inc %ffrwmaccuracy 2
n354=        if (%ffrsplevel = 8) { var %ffrwmstrength $floor($calc(160+ $rand(0,2) *40)) | set %ffrwmtier 4 }
n355=        else { var %ffrwmstrength $floor($calc(%ffrsplevel *20)) }
n356=        if (%ffrwmstrength < 160) { set %ffrwmtier 3 }
n357=        if (%ffrwmstrength < 100) { set %ffrwmtier 2 }
n358=        if (%ffrwmstrength < 60) { set %ffrwmtier 1 }
n359=      }
n360=      write -al3 WhiteSpell.txt Effect: Locked
n361=    }
n362=    elseif (%ffrwmdebuff = 9) {
n363=      if (%ffrwmelement = 2) { var %ffrwmelement 0 }
n364=      if (%ffrspellbinding = 1) {
n365=        if (%ffrsplevel < 8) { inc %ffrwmaccuracy 1 }
n366=        if (%ffrwmelement != 0) { inc %ffrwmaccuracy 2 }
n367=      }
n368=      write -al3 WhiteSpell.txt Effect: Stripped
n369=    }
n370=    ; Power Word Processing
n371=    elseif (%ffrwmdebuff = 10) {
n372=      var %ffrwmaccskip 1
n373=      :wmpword
n374=      if (%ffrspellbinding = 1) && (%ffrsplevel < 6) { echo 13 -s Debug: Bounced a Power Word before Level 6 | goto whitemagic }
n375=      if (%ffrwmafflict < 2) {
n376=        if ($rand(1,20) >= $rand(1,80)) {
n377=          /echo 8 -s Debug: FROG passed its saving roll ( $+ $v1 vs $v2 $+ )
n378=          write -al3 WhiteSpell.txt Effect: Power Word "Pacify"
n379=          write -al4 WhiteSpell.txt Element: Creature
n380=          write -al5 WhiteSpell.txt Target: Single Enemy
n381=          var %ffrwmspecial 1
n382=        }
n383=        else { /echo 13 -s Debug: FROG failed ( $+ $v1 vs $v2 $+ ). Making a new spell. | goto whitemagic }
n384=      }
n385=      elseif (%ffrwmafflict = 2) {
n386=        if ($rand(1,20) >= $rand(1,80)) {
n387=          /echo 8 -s Debug: CAST passed its saving roll ( $+ $v1 vs $v2 $+ )
n388=          write -al3 WhiteSpell.txt Effect: Power Word "Preserve"
n389=          write -al4 WhiteSpell.txt Element: Stone
n390=          write -al5 WhiteSpell.txt Target: Single Ally
n391=          var %ffrwmspecial 1
n392=        }
n393=        else { /echo 13 -s Debug: CAST failed ( $+ $v1 vs $v2 $+ ). Making a new spell. | goto whitemagic }
n394=      }
n395=      elseif (%ffrwmafflict = 3) { /echo 13 -s Debug: Rerolling a blank Power Word | var %ffrwmafflict $rand(2,8) | goto wmpword }
n396=      elseif (%ffrwmafflict = 4) { write -al3 WhiteSpell.txt Effect: Power Word "Blind" }
n397=      elseif (%ffrwmafflict = 5) { write -al3 WhiteSpell.txt Effect: Power Word "Stun" }
n398=      elseif (%ffrwmafflict = 6) { write -al3 WhiteSpell.txt Effect: Power Word "Naptime" }
n399=      elseif (%ffrwmafflict = 7) { write -al3 WhiteSpell.txt Effect: Power Word "Silence" }
n400=      elseif (%ffrwmafflict = 8) { write -al3 WhiteSpell.txt Effect: Power Word "Betray" }
n401=      else { /echo 4 -s Value %ffrwmafflict out of range for Power Words | /halt }
n402=    }
n403=    else { /echo 4 -s Value %ffrwmdebuff out of range for Debuffs | /halt }
n404=    ; Assigns an Element to the Debuff
n405=    ; Usually picks Status
n406=    ; Makes sure CAMP and CAST don't get overwritten
n407=    if (%ffrwmdebuff = 3) && (%ffrwmelement = 2) && (%ffrwmallies = 3) {
n408=      write -a14 WhiteSpell.txt Element: Fire
n409=      write -al5 WhiteSpell.txt Target: All Allies
n410=      var %ffrwmspecial 1
n411=    }
n412=    elseif (%ffrwmdebuff = 10) && (%ffrwmafflict = 2) { /echo 13 -s Debug: Element already set by CAST }
n413=    else {
n414=      if (%ffrwmelement = 1) { write -a14 WhiteSpell.txt Element: Status }
n415=      elseif (%ffrwmelement = 3) {
n416=        if (%ffrspellbinding = 1) { dec %ffrwmaccuracy 1 }
n417=        write -a14 WhiteSpell.txt Element: Time
n418=      }
n419=      elseif (%ffrwmelement = 4) {
n420=        if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 2 }
n421=        write -a14 WhiteSpell.txt Element: Earth
n422=      }
n423=      else {
n424=        if (%ffrwmdebuff != 9) && ($rand(1,10) != 10) { write -a14 WhiteSpell.txt Element: Status }
n425=        else {
n426=          write -al4 WhiteSpell.txt Non-Elemental
n427=          echo 8 -s Debug: Did not assign element to debuff
n428=        }
n429=      }
n430=    }
n431=  }
n432=  ; Healing Selection
n433=  ; 30% of Healing Spells are Status Removal
n434=  ; 10% of Healing Spells are CUR4
n435=  elseif (%ffrwmeffect = 4) {
n436=    :wmailment
n437=    if (%ffrwmheal > 6) { var %ffrwmstrskip 1 |
n438=      if (%ffrwmheal != 10) {
n439=        if (%ffrwmafflict = 0) {
n440=          if (%ffrspellbinding = 1) {
n441=            if (%ffrsplevel < 5) { echo 13 -s Debug: Bounced CLER before Level 5 | goto whitemagic }
n442=            elseif (%ffrsplevel isnum 5-6) { var %ffrwmallies 2 }
n443=            elseif (%ffrsplevel > 6) { var %ffrwmallies 3 }
n444=          }
n445=          write -a13 WhiteSpell.txt Effect: Refresh
n446=        }
n447=        ; "Refresh" excludes Death and Stone
n448=        ; Life must pass a 25% check
n449=        elseif (%ffrwmafflict = 1) {
n450=          if (h !isincs %ffrspflags) && ((%ffrspellbinding != 1) || (%ffrsplevel < 3)) {
n451=            if ($rand(1,4) = 4) { write -a13 WhiteSpell.txt Effect: Revive | var %ffrwmallies 2 }
n452=            else { /echo 13 -s Debug: Bounced at LIFE ( $+ $v1 vs 4) | goto whitemagic }
n453=          }
n454=          else {
n455=            write -al3 WhiteSpell.txt Effect: Revive
n456=            if (%ffrsplevel = 8) { var %ffrwmallies 3 }
n457=            else { var %ffrwmallies 2 }
n458=          }
n459=        }
n460=        ; Soft must win a coinflip
n461=        elseif (%ffrwmafflict = 2) {
n462=          if (h !isincs %ffrspflags) && ((%ffrspellbinding != 1) || (%ffrsplevel < 3)) {
n463=            if ($rand(0,1) = 1) { write -al3 WhiteSpell.txt Effect: Soften | var %ffrwmallies 2 }
n464=            else { /echo 13 -s Debug: Bounced at SOFT ( $+ $v1 vs 1) | goto whitemagic }
n465=          }
n466=          else {
n467=            write -al3 WhiteSpell.txt Effect: Soften
n468=            if (%ffrsplevel = 8) { var %ffrwmallies 3 }
n469=            else { var %ffrwmallies 2 }
n470=          }
n471=        }
n472=        elseif (%ffrwmafflict = 3) {
n473=          if (%ffrspellbinding = 1) {
n474=            if (%ffrsplevel < 2) { echo 13 -s Debug: Bounced PURE at Level 1 | goto whitemagic }
n475=            elseif (%ffrsplevel isnum 2-4) { var %ffrwmallies 2 }
n476=            elseif (%ffrsplevel isnum 5-6) { var %ffrwmallies 3 }
n477=            else { var %ffrwmafflict 0 | goto wmailment }
n478=          }
n479=          write -al3 WhiteSpell.txt Effect: Antidote
n480=        }
n481=        elseif (%ffrwmafflict = 4) {
n482=          if (%ffrspellbinding = 1) {
n483=            if (%ffrsplevel = 1) { var %ffrwmallies 2 }
n484=            elseif (%ffrsplevel isnum 2-6) { var %ffrwmallies 3 }
n485=            else { var %ffrwmafflict 0 | goto wmailment }
n486=          }
n487=          write -al3 WhiteSpell.txt Effect: Eyesight
n488=        }
n489=        elseif (%ffrwmafflict = 5) {
n490=          if (%ffrspellbinding = 1) { var %ffrwmafflict 3 | goto wmailment }
n491=          write -al3 WhiteSpell.txt Effect: Limber
n492=          var %ffrwmallies $rand(2,3)
n493=        }
n494=        elseif (%ffrwmafflict = 6) {
n495=          if (%ffrspellbinding = 1) {
n496=            if (%ffrsplevel = 1) { var %ffrwmallies 2 }
n497=            elseif (%ffrsplevel isnum 2-6) { var %ffrwmallies 3 }
n498=            else { var %ffrwmafflict 0 | goto wmailment }
n499=          }
n500=          else { var %ffrwmallies $rand(2,3) }
n501=          write -al3 WhiteSpell.txt Effect: Wake
n502=        }
n503=        elseif (%ffrwmafflict = 7) {
n504=          if (%ffrspellbinding = 1) {
n505=            if (%ffrsplevel < 4) { var %ffrwmallies 2 }
n506=            elseif (%ffrsplevel isnum 4-6) { var %ffrwmallies 3 }
n507=            else { var %ffrwmafflict 0 | goto wmailment }
n508=          }
n509=          else { var %ffrwmallies $rand(2,3) }
n510=          write -al3 WhiteSpell.txt Effect: Voice
n511=        }
n512=        elseif (%ffrwmafflict = 8) {
n513=          if (%ffrspellbinding = 1) { var %ffrwmafflict 3 | goto wmailment }
n514=          write -al3 WhiteSpell.txt Effect: Clarify
n515=          var %ffrwmallies $rand(2,3)
n516=        }
n517=        ; You cannot heal yourself of anything
n518=        ; but Poison and Dark, so other purifiers
n519=        ; make sure it targets an ally or the party
n520=        else { /echo 4 -s Value %ffrwmafflict out of range for Restoratives | /halt }
n521=      }
n522=    }
n523=    if (%ffrwmheal = 10) { 
n524=      if (%ffrspellbinding = 1) {
n525=        if (%ffrsplevel > 5) { write -al3 WhiteSpell.txt Effect: Full Restore }
n526=        else { echo 13 -s Debug: Spellbinding rejected CUR4 at Level %ffrsplevel | goto whitemagic }
n527=        if (%ffrsplevel = 8) && ($rand(1,20) >= $rand(1,80)) { var %ffrwmallies 3 }
n528=      }
n529=      else { write -al3 WhiteSpell.txt Effect: Full Restore }
n530=    }
n531=  }
n532=  ; Buff Selection
n533=  ; White Magic has trouble dealing damage
n534=  ; so FAST and TMPR are rarer and weaker
n535=  elseif (%ffrwmeffect = 5) {
n536=    ; Debug: echo 8 -s Debug: Passing through Buff (Buff: %ffrwmbuff $+ , Target: %ffrwmallies $+ )
n537=    if (%ffrwmbuff < 9) {
n538=      var %ffrwmstrskip 1
n539=      var %ffrwmallies 3
n540=      if ((%ffrspellbinding = 1) && (%ffrsplevel = 8)) || (%ffrwmbuff = 0) {
n541=        if (%ffrspellbinding != 1) && (%ffrwmstrength >= 80) { var %ffrwmallies 3 }
n542=        elseif (%ffrspellbinding = 1) && (%ffrsplevel < 5) { echo 13 -s Debug: Spellbinding bounced WALL at Level %ffrsplevel | goto whitemagic }
n543=        elseif (%ffrspellbinding = 1) && (%ffrsplevel = 8) && ($rand(1,20) >= $rand(1,80)) { var %ffrwmallies 3 }
n544=        else { var %ffrwmallies 2 }
n545=        echo 8 -s Checking for SANC rolls. ( $+ $v1 vs $v2 $+ )
n546=        if (%ffrspwall != 3) {
n547=          if (%ffrspresist > 4) {
n548=            echo 4 -s Resists already capped! Rerolling.
n549=            goto whitemagic
n550=          }
n551=          set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend all
n552=          set %ffrspwall 3
n553=          inc %ffrspresist 1
n554=        }
n555=        write -a13 WhiteSpell.txt Effect: Resist All
n556=        goto walljump
n557=      }
n558=      elseif (%ffrwmbuff = 1) {
n559=        var %ffrwmresist Status
n560=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n561=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n562=          if (%ffrspantiweak != 4) {
n563=            if (%ffrspresist > 4) {
n564=              echo 4 -s Resists already capped! Rerolling.
n565=              goto whitemagic
n566=            }
n567=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend hindrance }
n568=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend weak }
n569=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend psi }
n570=            set %ffrspantiweak 4
n571=            inc %ffrspresist 1
n572=          }
n573=        }
n574=        else { var %ffrspresmagic 1 }
n575=      }
n576=      elseif (%ffrwmbuff = 2) {
n577=        var %ffrwmresist Poison/Stone
n578=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n579=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n580=          if (%ffrspantibane != 5) {
n581=            if (%ffrspresist > 4) {
n582=              echo 4 -s Resists already capped! Rerolling.
n583=              goto whitemagic
n584=            }
n585=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend poisonous }
n586=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend bane }
n587=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend gas }
n588=            set %ffrspantibane 5
n589=            inc %ffrspresist 1
n590=          }
n591=        }
n592=        else { var %ffrspresdecay 1 }
n593=      }
n594=      elseif (%ffrwmbuff = 3) {
n595=        var %ffrwmresist Time
n596=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n597=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n598=          if (%ffrspantizap != 5) {
n599=            if (%ffrspresist > 4) {
n600=              echo 4 -s Resists already capped! Rerolling.
n601=              goto whitemagic
n602=            }
n603=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend dimension }
n604=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend time }
n605=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend age }
n606=            set %ffrspantizap 5
n607=            inc %ffrspresist 1
n608=          }
n609=        }
n610=        else { var %ffrspresdecay 1 }
n611=      }
n612=      elseif (%ffrwmbuff = 4) {
n613=        var %ffrwmresist Death
n614=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n615=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n616=          if (%ffrspantinecro != 4) {
n617=            if (%ffrspresist > 4) {
n618=              echo 4 -s Resists already capped! Rerolling.
n619=              goto whitemagic
n620=            }
n621=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend necrotic }
n622=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend evil }
n623=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend rub }
n624=            set %ffrspantinecro 4
n625=            inc %ffrspresist 1
n626=          }
n627=        }
n628=        else { 
n629=          if ($rand(1,2) = 1) { var %ffrspresmagic 1 }
n630=          else { var %ffrspresdecay 1 }
n631=        }
n632=      }
n633=      elseif (%ffrwmbuff = 5) {
n634=        var %ffrwmresist Fire
n635=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n636=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n637=          if (%ffrspantifire != 4) {
n638=            if (%ffrspresist > 4) {
n639=              echo 4 -s Resists already capped! Rerolling.
n640=              goto whitemagic
n641=            }
n642=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend burning }
n643=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend fire }
n644=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend hot }
n645=            set %ffrspantifire 4
n646=            inc %ffrspresist 1
n647=          }
n648=        }
n649=        else { var %ffrspresdragon 1 }
n650=      }
n651=      elseif (%ffrwmbuff = 6) {
n652=        var %ffrwmresist Ice
n653=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n654=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n655=          if (%ffrspantiice != 4) {
n656=            if (%ffrspresist > 4) {
n657=              echo 4 -s Resists already capped! Rerolling.
n658=              goto whitemagic
n659=            }
n660=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend freezing }
n661=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend cold }
n662=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend ice }
n663=            set %ffrspantiice 4
n664=            inc %ffrspresist 1
n665=          }
n666=        }
n667=        else { var %ffrspresdragon 1 }
n668=      }
n669=      elseif (%ffrwmbuff = 7) {
n670=        var %ffrwmresist Lit
n671=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n672=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n673=          if (%ffrspantilightning != 9) {
n674=            if (%ffrspresist > 4) {
n675=              echo 4 -s Resists already capped! Rerolling.
n676=              goto whitemagic
n677=            }
n678=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend lightning }
n679=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend volt }
n680=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend ion }
n681=            set %ffrspantilightning 9
n682=            inc %ffrspresist 1
n683=          }
n684=        }
n685=        else { var %ffrspresdragon 1 }
n686=      }
n687=      elseif (%ffrwmbuff = 8) {
n688=        var %ffrwmresist Earth
n689=        if (((%ffrspellbinding = 1) && (%ffrsplevel < 5)) || ((%ffrspellbinding != 1) && (%ffrwmstrength < 50))) {
n690=          if (((%ffrspellbinding = 1) && (%ffrsplevel = 1)) || ((%ffrspellbinding != 1) && (%ffrwmstrength <= 10))) { var %ffrwmallies 2 }
n691=          if (%ffrspantiquake != 5) {
n692=            if (%ffrspresist > 4) {
n693=              echo 4 -s Resists already capped! Rerolling.
n694=              goto whitemagic
n695=            }
n696=            elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend tectonic }
n697=            elseif (%ffrspresist isnum 1-3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend land }
n698=            else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend geo }
n699=            set %ffrspantiquake 5
n700=            inc %ffrspresist 1
n701=          }
n702=        }
n703=        else { var %ffrspresmagic 1 }
n704=      }
n705=      if (%ffrspresmagic = 1) {
n706=        var %ffrwmresist Magic
n707=        if (%ffrspantimagic != 5) {
n708=          if (%ffrspresist > 4) {
n709=            echo 4 -s Resists already capped! Rerolling.
n710=            goto whitemagic
n711=          }
n712=          elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend mortality }
n713=          elseif (%ffrspresist isnum 1-2) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend life }
n714=          elseif (%ffrspresist = 3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend magic }
n715=          else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend bio }
n716=          set %ffrspantimagic 5
n717=          inc %ffrspresist 1
n718=        }
n719=      }
n720=      elseif (%ffrspresdecay = 1) {
n721=        var %ffrwmresist Decay
n722=        if (%ffrspantitoxin != 5) {
n723=          if (%ffrspresist > 4) {
n724=            echo 4 -s Resists already capped! Rerolling.
n725=            goto whitemagic
n726=          }
n727=          elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend futility }
n728=          elseif (%ffrspresist isnum 1-2) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend doom }
n729=          elseif (%ffrspresist = 3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend waste }
n730=          else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend rot }
n731=          set %ffrspantitoxin 5
n732=          inc %ffrspresist 1
n733=        }
n734=      }
n735=      elseif (%ffrspresdragon = 1) {
n736=        var %ffrwmresist Dragon
n737=        if (%ffrspantidamage != 5) {
n738=          if (%ffrspresist > 4) {
n739=            echo 4 -s Resists already capped! Rerolling.
n740=            goto whitemagic
n741=          }
n742=          elseif (%ffrspresist = 0) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend elemental }
n743=          elseif (%ffrspresist isnum 1-2) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend wyrm }
n744=          elseif (%ffrspresist = 3) { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend spell }
n745=          else { set %ffrspresmsg $+ $calc(%ffrspresist +1) Defend wiz }
n746=          set %ffrspantidamage 5
n747=          inc %ffrspresist 1
n748=        }
n749=      }
n750=      write -al3 WhiteSpell.txt Effect: Resist %ffrwmresist
n751=    }
n752=    elseif (%ffrwmbuff = 9) { write -al3 WhiteSpell.txt Effect: Absorb Up }
n753=    elseif (%ffrwmbuff = 10) { write -al3 WhiteSpell.txt Effect: Absorb Up }
n754=    elseif (%ffrwmbuff = 11) { write -al3 WhiteSpell.txt Effect: Evade Up }
n755=    elseif (%ffrwmbuff = 12) { write -al3 WhiteSpell.txt Effect: Evade Up }
n756=    elseif (%ffrwmbuff = 13) { 
n757=      var %ffrwmstrskip 1
n758=      if (%ffrwmallies = 1) { write -al3 WhiteSpell.txt Effect: Double Hits }
n759=      else { /echo 13 -s Debug: Bounced at HAST (Target was $v1 instead of 1) | goto whitemagic }
n760=      ; White FAST is only allowed to cast on self
n761=      ; This implementation also makes it pass
n762=      ; a 1-in-3 check to succeed
n763=    }
n764=    elseif (%ffrwmbuff = 14) {
n765=      var %ffrwmaccskip 0
n766=      if (%ffrwmallies = 3) { var %ffrwmstrength $floor($calc(%ffrwmstrength * 0.15)) }
n767=      elseif (%ffrwmallies = 2) { var %ffrwmstrength $floor($calc(%ffrwmstrength * 0.20)) }
n768=      else { var %ffrwmstrength $floor($calc(%ffrwmstrength * 0.25)) }
n769=      write -al3 WhiteSpell.txt Effect: Attack Up
n770=      ; White TMPR is cut down in power by at least 75%
n771=    }
n772=    else { /echo 4 -s Value %ffrwmbuff out of range for Buffs | /halt }
n773=  }
n774=  ; Damage Element Selection
n775=  ; White Magic struggles with damage all-around
n776=  ; so it doesn't have access to Fire, Ice, nor Lit
n777=  ; it also can't access Death, but that's flavour
n778=  elseif (%ffrwmeffect = 1) {
n779=    if (%ffrwmelement = 1) { write -al4 WhiteSpell.txt Element: Status }
n780=    elseif (%ffrwmelement = 2) {
n781=      if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 2 }
n782=      write -al4 WhiteSpell.txt Element: Stone
n783=    }
n784=    elseif (%ffrwmelement = 3) {
n785=      if (%ffrspellbinding = 1) { dec %ffrwmaccuracy 1 }
n786=      write -al4 WhiteSpell.txt Element: Time
n787=    }
n788=    elseif (%ffrwmelement = 4) {
n789=      if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 2 }
n790=      write -al4 WhiteSpell.txt Element: Earth
n791=    }
n792=    elseif (%ffrwmelement = 0) && ($rand(1,20) >= $rand(1,20)) {
n793=      write -al4 WhiteSpell.txt Element: None
n794=      if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 1 }
n795=    }
n796=    else { /echo 13 -s FADE failed saving roll ( $+ $v1 vs $v2 $+ ). Rolling new spell. | goto whitemagic }
n797=  }
n798=  ; Harm Undead is always non-elemental
n799=  ; I thought about giving it Fire, but
n800=  ; that would just be bonus damage
n801=  ; (except on Lich2)
n802=  elseif (%ffrwmeffect = 2) { write -a14 WhiteSpell.txt Element: None }
n803=  else { /echo 4 -s Value %ffrwmeffect out of range for Damage Type | /halt }
n804=
n805=  ; Writes the Spell Target to a file
n806=  :walljump
n807=  if (%ffrwmspecial != 1) {
n808=    if (%ffrwmeffect < 4) {
n809=      if (%ffrwmenemies = 1) {
n810=        if (%ffrspellbinding = 1) { inc %ffrwmaccuracy 1 }
n811=        write -al5 WhiteSpell.txt Target: Single Enemy
n812=      }
n813=      elseif (%ffrwmenemies = 2) { write -al5 WhiteSpell.txt Target: Enemy Party }
n814=      else { /echo 4 -s Value %ffrwmenemies out of range for Enemy Target | /halt }
n815=    }
n816=    elseif (%ffrwmeffect > 3) {
n817=      if (%ffrwmallies = 1) { 
n818=        if (%ffrwmeffect = 4) { inc %ffrwmallies $rand(1,2) }
n819=        else {
n820=          write -al5 WhiteSpell.txt Target: Caster
n821=          if (%ffrspellbinding = 1) {
n822=            if ($read(WhiteSpell.txt,w,*absorb*)) {
n823=              if (%ffrsplevel = 8) { var %ffrwmstrength 64 | set %ffrwmtier 4 }
n824=              if (%ffrsplevel < 8) { var %ffrwmstrength 32 | set %ffrwmtier 3 }
n825=              if (%ffrsplevel < 5) { var %ffrwmstrength 24 | set %ffrwmtier 2 }
n826=              if (%ffrsplevel < 3) { var %ffrwmstrength 16 | set %ffrwmtier 1 }
n827=            }
n828=            elseif ($read(WhiteSpell.txt,w,*evade*)) {
n829=              if (%ffrsplevel = 8) { var %ffrwmstrength 240 | set %ffrwmtier 4 }
n830=              if (%ffrsplevel < 8) { var %ffrwmstrength 120 | set %ffrwmtier 3 }
n831=              if (%ffrsplevel < 5) { var %ffrwmstrength 80 | set %ffrwmtier 2 }
n832=              if (%ffrsplevel < 3) { var %ffrwmstrength 60 | set %ffrwmtier 1 }
n833=            }
n834=            elseif ($read(WhiteSpell.txt,w,*attack*)) {
n835=              if (%ffrsplevel = 8) { var %ffrwmstrength 16 | set %ffrwmtier 4 }
n836=              if (%ffrsplevel < 8) { var %ffrwmstrength 12 | set %ffrwmtier 3 }
n837=              if (%ffrsplevel < 5) { var %ffrwmstrength 10 | set %ffrwmtier 2 }
n838=              if (%ffrsplevel < 3) { var %ffrwmstrength 8 | set %ffrwmtier 1 }
n839=            }
n840=          }
n841=          elseif ($read(WhiteSpell.txt,w,*evade*)) { var %ffrwmstrength $floor($calc( %ffrwmstrength *2)) }
n842=        }
n843=      }
n844=      if (%ffrwmallies = 2) { 
n845=        if ($read(WhiteSpell.txt,w,*absorb*)) || ($read(WhiteSpell.txt,w,*evade*)) { inc %ffrwmallies 1 }
n846=        else {
n847=          write -al5 WhiteSpell.txt Target: Single Ally
n848=          if (%ffrwmeffect = 4) {
n849=            if (%ffrspellbinding = 1) || (h isincs %ffrspflags) {
n850=              if (%ffrsplevel != 8) {
n851=                var %ffrwmstrength $floor($calc(2^ (3.5 + %ffrsplevel /2) + (%ffrsplevel - 1) /2 ))
n852=              }
n853=              else { var %ffrwmstrength $rand(66,184) }
n854=            }
n855=            else { var %ffrwmstrength $floor($calc( %ffrwmstrength *1.6)) }
n856=            if (%ffrwmstrength > 32) { set %ffrwmtier 2 }
n857=            else { set %ffrwmtier 1 }
n858=            if (%ffrwmstrength > 65) { set %ffrwmtier 3 }
n859=          }
n860=          else {
n861=            if (%ffrspellbinding = 1) {
n862=              dec %ffrwmaccuracy 1
n863=              if (%ffrsplevel = 8) { var %ffrwmstrength 12 | set %ffrwmtier 4 }
n864=              if (%ffrsplevel < 8) { var %ffrwmstrength 10 | set %ffrwmtier 3 }
n865=              if (%ffrsplevel < 5) { var %ffrwmstrength 8 | set %ffrwmtier 2 }
n866=              if (%ffrsplevel < 3) { var %ffrwmstrength 5 | set %ffrwmtier 1 }
n867=            }
n868=          }
n869=        }
n870=      }
n871=      if (%ffrwmallies = 3) {
n872=        write -al5 WhiteSpell.txt Target: All Allies
n873=        if (%ffrwmeffect = 4) {
n874=          if (%ffrspellbinding = 1) || (h isincs %ffrspflags) {
n875=            if (%ffrsplevel != 8) { var %ffrwmstrength $floor($calc(6*2^ (%ffrsplevel /2-0.5))) }
n876=            else { var %ffrwmstrength $rand(48,135) }
n877=          }
n878=          else { dec %ffrwmstrength 4 }
n879=          if (%ffrwmstrength > 23) { set %ffrwmtier 2 }
n880=          else { set %ffrwmtier 1 }
n881=          if (%ffrwmstrength > 47) { set %ffrwmtier 3 }
n882=        }
n883=        elseif (%ffrwmeffect = 5) && (%ffrspellbinding = 1) {
n884=          if ($read(WhiteSpell.txt,w,*absorb*)) {
n885=            if (%ffrsplevel = 8) { var %ffrwmstrength 24 | set %ffrwmtier 4 }
n886=            if (%ffrsplevel < 8) { var %ffrwmstrength 16 | set %ffrwmtier 3 }
n887=            if (%ffrsplevel < 5) { var %ffrwmstrength 12 | set %ffrwmtier 2 }
n888=            if (%ffrsplevel < 3) { var %ffrwmstrength 8 | set %ffrwmtier 1 }
n889=          }
n890=          elseif ($read(WhiteSpell.txt,w,*evade*)) {
n891=            if (%ffrsplevel = 8) { var %ffrwmstrength 80 | set %ffrwmtier 4 }
n892=            if (%ffrsplevel < 8) { var %ffrwmstrength 60 | set %ffrwmtier 3 }
n893=            if (%ffrsplevel < 5) { var %ffrwmstrength 40 | set %ffrwmtier 2 }
n894=            if (%ffrsplevel < 3) { var %ffrwmstrength 30 | set %ffrwmtier 1 }
n895=          }
n896=          elseif ($read(WhiteSpell.txt,w,*attack*)) {
n897=            var %ffrwmaccskip 0
n898=            dec %ffrwmaccuracy 2
n899=            if (%ffrsplevel = 8) { var %ffrwmstrength 10 | set %ffrwmtier 4 }
n900=            if (%ffrsplevel < 8) { var %ffrwmstrength 8 | set %ffrwmtier 3 }
n901=            if (%ffrsplevel < 5) { var %ffrwmstrength 5 | set %ffrwmtier 2 }
n902=            if (%ffrsplevel < 3) { var %ffrwmstrength 3 | set %ffrwmtier 1 }  
n903=          }
n904=        }
n905=      }
n906=    }
n907=    else { /echo 4 -s Value %ffrwmeffect out of range for Power Assignment | /halt }
n908=  }
n909=  else { /echo 13 -s Debug: Target already assigned by Rare Spell }
n910=
n911=  ; Accuracy Balance
n912=  ; Defines the appropriate tiers of Accuracy for Spellbooks
n913=  ; Accuracy no longer caps
n914=  if (%ffrspellbinding = 1) {
n915=    if (%ffrwmaccuracy < 2) { var %ffrwmaccsay 0 }
n916=    elseif (%ffrwmaccuracy = 2) { var %ffrwmaccsay 5 }
n917=    elseif (%ffrwmaccuracy = 3) { var %ffrwmaccsay 8 }
n918=    elseif (%ffrwmaccuracy = 4) { var %ffrwmaccsay 16 }
n919=    elseif (%ffrwmaccuracy = 5) { var %ffrwmaccsay 24 }
n920=    elseif (%ffrwmaccuracy = 6) { var %ffrwmaccsay 32 }
n921=    elseif (%ffrwmaccuracy = 7) { var %ffrwmaccsay 40 }
n922=    elseif (%ffrwmaccuracy = 8) { var %ffrwmaccsay 48 }
n923=    elseif (%ffrwmaccuracy = 9) { var %ffrwmaccsay 64 }
n924=    elseif (%ffrwmaccuracy = 10) { var %ffrwmaccsay 107 }
n925=    elseif (%ffrwmaccuracy = 11) { var %ffrwmaccsay 128 }
n926=    elseif (%ffrwmaccuracy = 12) { var %ffrwmaccsay 152 }
n927=    elseif (%ffrwmaccuracy = 13) { var %ffrwmaccsay 175 }
n928=    elseif (%ffrwmaccuracy = 14) { var %ffrwmaccsay 210 }
n929=    else { var %ffrwmaccsay 255 | echo 8 -s Debug: Assumed Accuracy was Auto-Hit }
n930=  }
n931=
n932=  ; Cleanup
n933=
n934=  ; Writes only information relevant to the spell
n935=  if (%ffrwmstrskip = 0) && (%ffrwmaccskip = 0) { write -a16 WhiteSpell.txt Power: %ffrwmstrength | write -al7 WhiteSpell.txt Acc Bonus: %ffrwmaccsay }
n936=  elseif (%ffrwmstrskip = 0) && (%ffrwmaccskip = 1) { write -a16 WhiteSpell.txt Power: %ffrwmstrength }
n937=  elseif (%ffrwmstrskip = 1) && (%ffrwmaccskip = 0) { write -a16 WhiteSpell.txt Acc Bonus: %ffrwmaccsay }
n938=
n939=  ; Doesn't open the file when Spellbinding
n940=  ; because that would open 64 files
n941=  ; Debug: if (%ffrspellbinding = 1) && (%ffrwmbuff = 14) { /run WhiteSpell.txt | halt }
n942=  if ($read(WhiteSpell.txt,w,*resist*)) { echo 8 -s Resist ID: %ffrwmbuff }
n943=  if (%ffrwmaccsay > 64) { set %ffrspredharm 1 }
n944=  if (%ffrnowrite != 1) { /run WhiteSpell.txt | unset %ffr* }
n945=}
