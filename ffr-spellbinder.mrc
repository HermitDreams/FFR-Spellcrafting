[aliases]
n0=/spell { ; Final Fantasy Randomizer Spellbinder. IRC Script by Linkshot
n1=  ; Flag List
n2=  ; -e: Enemy Slot sanity checker
n3=  ; -h: Out-of-battle Healing preservation
n4=  ; -b: Black AoE Amount sanity checker
n5=  ; -C: FIRE Slot sanity checker for Confused status
n6=  ; -i: Item Magic assignment
n7=  ; -r: Alter Permissions
n8=  ; -S: LOK2 & HEL2 fix compatibility
n9=  ; -x: Turns off spellbook logic
n10=  set %ffrspflags -
n11=  if (b2 isincs $1) { set %ffrspflags %ffrspflags $+ b2 }
n12=  elseif (b isincs $1) { set %ffrspflags %ffrspflags $+ b }
n13=  if (C isincs $1) { set %ffrspflags %ffrspflags $+ C }
n14=  if (e isincs $1) { set %ffrspflags %ffrspflags $+ e }
n15=  if (h isincs $1) { set %ffrspflags %ffrspflags $+ h }
n16=  if (i isincs $1) { set %ffrspflags %ffrspflags $+ i }
n17=  if (r isincs $1) { set %ffrspflags %ffrspflags $+ r }
n18=  if (S isincs $1) { set %ffrspflags %ffrspflags $+ S }
n19=  if (x isincs $1) { set %ffrspflags %ffrspflags $+ x }
n20=  write -c SpellTable.txt
n21=  if (x !isincs $1) { set %ffrspellbinding 1 }
n22=  set %ffrnowrite 1
n23=  set %ffrspblackAoE 0
n24=  set %ffrsplevel 1
n25=  set %ffrspslot 0
n26=  set %ffrspline 0
n27=  set %ffrspmagic white
n28=  set %ffrspresist 0
n29=  set %ffrspantiweak 0
n30=  set %ffrspantibane 0
n31=  set %ffrspantizap 0
n32=  set %ffrspantinecro 0
n33=  set %ffrspantifire 0
n34=  set %ffrspantiice 0
n35=  set %ffrspantilightning 0
n36=  set %ffrspantiquake 0
n37=  set %ffrspantimagic 0
n38=  set %ffrspantitoxin 0
n39=  set %ffrspantidamage 0
n40=  set %ffrspwall 0
n41=  set %ffrspbatmsgloop 0
n42=  while (%ffrspbatmsgloop !>= 77) {
n43=    inc %ffrspbatmsgloop 1
n44=    if (%ffrspbatmsgloop != 76) { set %ffrspunmsg $+ %ffrspbatmsgloop [Unassigned] }
n45=  }
n46=  set %ffrspmsg76 Go mad [Unused]
n47=  :sploop
n48=  set %ffrspreroll 0
n49=  set %ffrspreslength $calc(25- (%ffrspantiweak + %ffrspantibane + %ffrspantizap + %ffrspantinecro + %ffrspantifire + %ffrspantiice + %ffrspantilightning + %ffrspantiquake + %ffrspantidamage + %ffrspantimagic + %ffrspantitoxin + %ffrspwall ))
n50=  inc %ffrspslot 1
n51=  inc %ffrspline 1
n52=  if (%ffrsplevel = 5) && (%ffrspslot = 3) && (%ffrspmagic = black) { write -al $+ %ffrspline SpellTable.txt WARP | goto sploop }
n53=  elseif (%ffrsplevel = 6) && (%ffrspslot = 2) && (%ffrspmagic = white) { write -al $+ %ffrspline SpellTable.txt EXIT | goto sploop }
n54=  :spdupe
n55=  if (%ffrspmagic = white) {
n56=    /wmag
n57=    if (status isin $read(WhiteSpell.txt,w,*element*)) { var %ffrspgfxclr 6 (Magenta) }
n58=    elseif (earth isin $read(WhiteSpell.txt,w,*element*)) { var %ffrspgfxclr 10 (Bright Green) }
n59=    elseif (time isin $read(WhiteSpell.txt,w,*element*)) { var %ffrspgfxclr 11 (Dark Green) }
n60=    elseif (stone isin $read(WhiteSpell.txt,w,*element*)) { var %ffrspgfxclr 1 (White) }
n61=    else { var %ffrspgfxclr 5 (Pink) }
n62=    if ( $read(WhiteSpell.txt,1) = Type: Damage) {
n63=      if ($read(WhiteSpell.txt,w,*auto*)) {
n64=        var %ffrspbatmsg 43 (Critical hit!!)
n65=        var %ffrspcommand set % $+ ffrspunmsg43 [Assigned]
n66=      }
n67=      else { var %ffrspbatmsg 0; None }
n68=      var %ffrsptype fight
n69=      var %ffrspgfxshape 18
n70=      if ($read(WhiteSpell.txt,w,*party*)) { inc %ffrspgfxshape 1 }
n71=      if ( $read(WhiteSpell.txt,2) = Element: None) {
n72=        if ( $read(WhiteSpell.txt,3) = Target: Single Enemy) { 
n73=          if (%ffrwmtier > 1) { var %ffrspname SUN $+ %ffrwmtier }
n74=          else { var -n %ffrspname SUN  $+ }
n75=        }
n76=        elseif ( $read(WhiteSpell.txt,3) = Target: Enemy Party) {
n77=          if (%ffrwmtier = 4) { var -n %ffrspname FADE }
n78=          elseif (%ffrwmtier > 1) { var %ffrspname RAZ $+ %ffrwmtier }
n79=          else { var -n %ffrspname RAZE }
n80=        }
n81=      }
n82=      elseif ( $read(WhiteSpell.txt,2) = Element: Status) {
n83=        if ( $read(WhiteSpell.txt,3) = Target: Single Enemy) { 
n84=          if (%ffrwmtier > 1) { var %ffrspname HRT $+ %ffrwmtier }
n85=          else { var -n %ffrspname HURT }
n86=        }
n87=        elseif ( $read(WhiteSpell.txt,3) = Target: Enemy Party) {
n88=          if (%ffrwmtier = 4) { var -n %ffrspname ACHE }
n89=          elseif (%ffrwmtier > 1) { var %ffrspname PAN $+ %ffrwmtier }
n90=          else { var -n %ffrspname PAIN }
n91=        }
n92=      }
n93=      elseif ( $read(WhiteSpell.txt,2) = Element: Stone) {
n94=        if ( $read(WhiteSpell.txt,3) = Target: Single Enemy) { 
n95=          if (%ffrwmtier > 1) { var %ffrspname LAZ $+ %ffrwmtier }
n96=          else { var -n %ffrspname LAZR }
n97=        }
n98=        elseif ( $read(WhiteSpell.txt,3) = Target: Enemy Party) {
n99=          if (%ffrwmtier = 4) { var -n %ffrspname WAIL }
n100=          elseif (%ffrwmtier > 1) { var %ffrspname BEM $+ %ffrwmtier }
n101=          else { var -n %ffrspname BEAM }
n102=        }
n103=      }
n104=      elseif ( $read(WhiteSpell.txt,2) = Element: Time) {
n105=        if ( $read(WhiteSpell.txt,3) = Target: Single Enemy) { 
n106=          if (%ffrwmtier > 1) { var %ffrspname SQZ $+ %ffrwmtier }
n107=          else { var -n %ffrspname SQEZ }
n108=        }
n109=        elseif ( $read(WhiteSpell.txt,3) = Target: Enemy Party) {
n110=          if (%ffrwmtier = 4) { var -n %ffrspname KOMP }
n111=          elseif (%ffrwmtier > 1) { var %ffrspname TIM $+ %ffrwmtier }
n112=          else { var -n %ffrspname TIME }
n113=        }
n114=      }
n115=      elseif ( $read(WhiteSpell.txt,2) = Element: Earth) {
n116=        if ( $read(WhiteSpell.txt,3) = Target: Single Enemy) { 
n117=          if (%ffrwmtier > 1) { var %ffrspname LND $+ %ffrwmtier }
n118=          else { var -n %ffrspname LAND }
n119=        }
n120=        elseif ( $read(WhiteSpell.txt,3) = Target: Enemy Party) {
n121=          if (%ffrwmtier = 4) { var -n %ffrspname GAIA }
n122=          elseif (%ffrwmtier > 1) { var %ffrspname GEO $+ %ffrwmtier }
n123=          else { var -n %ffrspname GEO  $+ }
n124=        }
n125=      }
n126=      else { echo 13 -s Debug: Unidentified White Damage ( $+ $read(WhiteSpell.txt,2) $+ ) | write -al $+ %ffrspline SpellTable.txt WHITE DAMAGE }
n127=    }
n128=    elseif ( $read(WhiteSpell.txt,1) = Type: Harm Undead) {
n129=      if ($read(WhiteSpell.txt,w,*auto*)) {
n130=        var %ffrspbatmsg 43 (Critical hit!!)
n131=        var %ffrspcommand set % $+ ffrspunmsg43 [Assigned]
n132=      }
n133=      else { var %ffrspbatmsg 0; None }
n134=      var %ffrsptype fight
n135=      var %ffrspgfxshape 18
n136=      if ($read(WhiteSpell.txt,w,*party*)) { inc %ffrspgfxshape 1 }
n137=      if ((%ffrspellbinding = 1) && (2 // %ffrsplevel)) || (%ffrspredharm = 1) { var %ffrspgfxclr 7 (Red) }
n138=      else { var %ffrspgfxclr 12 (Pale Cyan) }
n139=      if ( $read(WhiteSpell.txt,3) = Target: Single Enemy) { 
n140=        if (%ffrwmtier > 1) { var %ffrspname JUG $+ %ffrwmtier }
n141=        else { var -n %ffrspname JUDG }
n142=      }
n143=      elseif ( $read(WhiteSpell.txt,3) = Target: Enemy Party) {
n144=        if (%ffrsplevel = 8) { var %ffrspname VANQ }
n145=        elseif (%ffrwmtier > 1) { var %ffrspname HRM $+ %ffrwmtier }
n146=        else { var -n %ffrspname HARM }
n147=      }
n148=    }
n149=    elseif ( $read(WhiteSpell.txt,1) = Type: Heal) {
n150=      var %ffrsptype blank
n151=      var %ffrspgfxshape 24
n152=      if ($read(WhiteSpell.txt,w,*single*)) { inc %ffrspgfxshape 1 }
n153=      if ( $read(WhiteSpell.txt,2) = Effect: Revive) {
n154=        var %ffrspbatmsg 74 (Ineffective now)
n155=        var %ffrspcommand set % $+ ffrspunmsg74 [Assigned]
n156=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var %ffrspgfxclr 12 (Pale Cyan) | var -n %ffrspname LIFE }
n157=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) {
n158=          var %ffrspgfxshape 15
n159=          var %ffrspgfxclr 5 (Pink)
n160=          var -n %ffrspname LIF2
n161=        }
n162=      }
n163=      elseif ( $read(WhiteSpell.txt,2) = Effect: Soften) {
n164=        var %ffrspgfxclr 1 (White)
n165=        var %ffrspbatmsg 74 (Ineffective now)
n166=        var %ffrspcommand set % $+ ffrspunmsg74 [Assigned]
n167=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname SOFT }
n168=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname SOFa }
n169=      }
n170=      elseif ( $read(WhiteSpell.txt,2) = Effect: Antidote) || ( $read(WhiteSpell.txt,2) = Effect: Clarify) {
n171=        var %ffrspgfxclr 10 (Bright Green)
n172=        var %ffrspbatmsg 0; None
n173=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname PURE }
n174=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname PURa }
n175=        elseif ( $read(WhiteSpell.txt,3) = Target: Caster) { var -n %ffrspname REST }
n176=      }
n177=      elseif ( $read(WhiteSpell.txt,2) = Effect: Eyesight) {
n178=        var %ffrspgfxclr 7 (Red)
n179=        var %ffrspbatmsg 0; None
n180=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname LAMP }
n181=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname LMPa }
n182=        elseif ( $read(WhiteSpell.txt,3) = Target: Caster) { var -n %ffrspname WASH }
n183=      }
n184=      elseif ( $read(WhiteSpell.txt,2) = Effect: Limber) {
n185=        var %ffrspgfxclr 6 (Magenta)
n186=        var %ffrspbatmsg 0; None
n187=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname MEND }
n188=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname MNDa }
n189=      }
n190=      elseif ( $read(WhiteSpell.txt,2) = Effect: Wake) {
n191=        var %ffrspgfxclr 11 (Dark Green)
n192=        var %ffrspbatmsg 0; None
n193=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname WAKE }
n194=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname WAKa }
n195=      }
n196=      elseif ( $read(WhiteSpell.txt,2) = Effect: Voice) {
n197=        var %ffrspgfxclr 2 (Light Blue)
n198=        var %ffrspbatmsg 0; None
n199=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname VOX  $+ }
n200=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname VOXa }
n201=      }
n202=      elseif ( $read(WhiteSpell.txt,2) = Effect: Refresh) {
n203=        var %ffrspgfxclr 5 (Pink)
n204=        var %ffrspbatmsg 0; None
n205=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname CLER }
n206=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname CLRa }
n207=        elseif ( $read(WhiteSpell.txt,3) = Target: Caster) { var -n %ffrspname BATH }
n208=      }
n209=      elseif ( $read(WhiteSpell.txt,2) = Effect: Full Restore) {
n210=        var %ffrspgfxclr 3 (Dark Blue)
n211=        var %ffrspbatmsg 24 (HP max!)
n212=        var %ffrspcommand set % $+ ffrspunmsg24 [Assigned]
n213=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) {
n214=          var %ffrspgfxshape 15
n215=          if (%ffrspellbinding = 1) { var -n %ffrspname CUR4 }
n216=          else { var -n %ffrspname MAX }
n217=        }
n218=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) {
n219=          var %ffrspgfxshape 16
n220=          if (%ffrspellbinding = 1) { var -n %ffrspname HEL4 }
n221=          else { var -n %ffrspname MAXa }
n222=        }
n223=        elseif ( $read(WhiteSpell.txt,3) = Target: Caster) {
n224=          var %ffrspgfxclr 11 (Dark Green)
n225=          var %ffrspgfxshape 15
n226=          if (%ffrspellbinding = 1) { var -n %ffrspname RGN4 }
n227=          else { var -n %ffrspname UNDO }
n228=        }
n229=      }
n230=      else {
n231=        var %ffrsptype help
n232=        if (%ffrwmtier = 1) { var %ffrspgfxclr 10 (Bright Green) }
n233=        elseif (%ffrwmtier = 2) { var %ffrspgfxclr 12 (Pale Cyan) }
n234=        elseif (%ffrwmtier = 3) { var %ffrspgfxclr 13 (Bright Cyan) }
n235=        var %ffrspbatmsg 1 (HP up!)
n236=        var %ffrspcommand set % $+ ffrspunmsg1 [Assigned]
n237=        if ( $read(WhiteSpell.txt,2) = Target: Single Ally) {
n238=          var %ffrspgfxshape 15
n239=          if (%ffrwmtier > 1) { var %ffrspname CUR $+ %ffrwmtier }
n240=          else { var -n %ffrspname CURE }
n241=        }
n242=        elseif ( $read(WhiteSpell.txt,2) = Target: All Allies) {
n243=          var %ffrspgfxshape 16
n244=          if (%ffrwmtier > 1) { var %ffrspname HEL $+ %ffrwmtier }
n245=          else { var -n %ffrspname HEAL }
n246=        }
n247=        elseif ( $read(WhiteSpell.txt,2) = Target: Caster) { var -n %ffrspname RGEN }
n248=        else { echo 13 -s Debug: Unidentified Healing Spell ( $+ $read(WhiteSpell.txt,2) $+ ) |  write -al $+ %ffrspline SpellTable.txt WHITE HEAL }
n249=      }
n250=    }
n251=    elseif ( $read(WhiteSpell.txt,1) = Type: Buff) {
n252=      var %ffrsptype help
n253=      var %ffrspgfxshape 12
n254=      if ($read(WhiteSpell.txt,w,*allies*)) { inc %ffrspgfxshape 1 }
n255=      if ( $read(WhiteSpell.txt,2) = Effect: Attack Up) {
n256=        var %ffrsptype fight
n257=        var %ffrspgfxclr 12 (Pale Cyan)
n258=        var %ffrspbatmsg 27 (Weapon became enchanted)
n259=        var %ffrspcommand set % $+ ffrspunmsg27 [Assigned]
n260=        if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { 
n261=          if (%ffrwmtier > 1) { var %ffrspname BLS $+ %ffrwmtier }
n262=          else { var -n %ffrspname BLES }
n263=        }
n264=        elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) {
n265=          if (%ffrwmtier > 1) { var %ffrspname VIG $+ %ffrwmtier }
n266=          else { var -n %ffrspname VIGR }
n267=        }
n268=        elseif ( $read(WhiteSpell.txt,3) = Target: Caster) {
n269=          var %ffrspgfxclr 1 (White)
n270=          if (%ffrwmtier > 1) { var %ffrspname FOC $+ %ffrwmtier }
n271=          else { var -n %ffrspname FOCS }
n272=        }
n273=      }
n274=      elseif ( $read(WhiteSpell.txt,2) = Effect: Double Hits) {
n275=        var %ffrsptype blank
n276=        var -n %ffrspname HAST
n277=        var %ffrspgfxclr 11 (Dark Green)
n278=        var %ffrspbatmsg 18 (Quick shot)
n279=        var %ffrspcommand set % $+ ffrspunmsg18 [Assigned]
n280=      }
n281=      elseif ( $read(WhiteSpell.txt,2) = Effect: Absorb Up) {
n282=        var %ffrspgfxclr 10 (Bright Green)
n283=        var %ffrspbatmsg 2 (Armor up)
n284=        var %ffrspcommand set % $+ ffrspunmsg2 [Assigned]
n285=        if ( $read(WhiteSpell.txt,3) = Target: All Allies) {
n286=          if (%ffrwmtier > 1) { var %ffrspname FOG $+ %ffrwmtier }
n287=          else { var -n %ffrspname FOG  $+ }
n288=        }
n289=        elseif ( $read(WhiteSpell.txt,3) = Target: Caster) {
n290=          if (%ffrwmtier > 1) { var %ffrspname BUK $+ %ffrwmtier }
n291=          else { var -n %ffrspname BUKL }
n292=        }
n293=      }
n294=      elseif ( $read(WhiteSpell.txt,2) = Effect: Evade Up) {
n295=        var %ffrspgfxclr 3 (Dark Blue)
n296=        var %ffrspbatmsg 3 (Easy to dodge)
n297=        var %ffrspcommand set % $+ ffrspunmsg3 [Assigned]
n298=        if ( $read(WhiteSpell.txt,3) = Target: All Allies) {
n299=          if (%ffrwmtier > 1) { var %ffrspname INV $+ %ffrwmtier }
n300=          else { var -n %ffrspname INVS }
n301=        }
n302=        elseif ( $read(WhiteSpell.txt,3) = Target: Caster) {
n303=          if (%ffrwmtier > 1) { var %ffrspname RUS $+ %ffrwmtier }
n304=          else { var -n %ffrspname RUSE }
n305=        }
n306=      }
n307=      elseif ($read(WhiteSpell.txt,w,*resist*)) {
n308=        var %ffrsptype blank
n309=        ; Make some complex message slot assignment system here
n310=        if ( $read(WhiteSpell.txt,2) = Effect: Resist Status) {
n311=          var %ffrspgfxclr 6 (Magenta)
n312=          var %ffrspbatmsg Defend weak
n313=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname BRAC }
n314=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname AWEK }
n315=        }
n316=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Poison/Stone) {
n317=          var %ffrspgfxclr 4 (Purple)
n318=          var %ffrspbatmsg Defend smoke
n319=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname VCIN }
n320=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname ABAN }
n321=        }
n322=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Time) {
n323=          var %ffrspgfxclr 11 (Dark Green)
n324=          var %ffrspbatmsg Defend exile
n325=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname XCEP }
n326=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname AZAP }
n327=        }
n328=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Death) {
n329=          var %ffrspgfxclr 14 (Grey)
n330=          var %ffrspbatmsg Defend evil
n331=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname SAFE }
n332=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname ANEC }
n333=        }
n334=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Fire) {
n335=          var %ffrspgfxclr 7 (Red)
n336=          var %ffrspbatmsg Defend fire
n337=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname DOUS }
n338=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname AFIR }
n339=        }
n340=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Ice) {
n341=          var %ffrspgfxclr 13 (Bright Cyan)
n342=          var %ffrspbatmsg Defend cold
n343=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname COAT }
n344=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname AICE }
n345=        }
n346=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Lit) {
n347=          var %ffrspgfxclr 3 (Dark Blue)
n348=          var %ffrspbatmsg Defend lightning
n349=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname OHM  }
n350=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname ALIT }
n351=        }
n352=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Earth) {
n353=          var %ffrspgfxclr 8 (Orange)
n354=          var %ffrspbatmsg Defend crack
n355=          if ( $read(WhiteSpell.txt,3) = Target: Single Ally) { var -n %ffrspname LIFT }
n356=          elseif ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname AQAK }
n357=        }
n358=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Dragon) { var %ffrspgfxclr 2 (Light Blue) | var %ffrspbatmsg Defend spell | var -n %ffrspname ADMG }
n359=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Magic) { var %ffrspgfxclr 6 (Magenta) | var %ffrspbatmsg Defend magic | var -n %ffrspname ARUB }
n360=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist Decay) { var %ffrspgfxclr 4 (Purple) | var %ffrspbatmsg Defend aging | var -n %ffrspname ATOX }
n361=        elseif ( $read(WhiteSpell.txt,2) = Effect: Resist All) {
n362=          var %ffrspgfxclr 5 (Pink)
n363=          var %ffrspbatmsg Defend all
n364=          if ( $read(WhiteSpell.txt,3) = Target: All Allies) { var -n %ffrspname SANC }
n365=          else { var -n %ffrspname WALL }
n366=        }
n367=      }
n368=      else { echo 13 -s Debug: Unidentified Support ( $+ $read(WhiteSpell.txt,2) $+ ) | write -a1 $+ %ffrspline SpellTable.txt WHITE BUFF }
n369=    }
n370=    elseif ( $read(WhiteSpell.txt,1) = Type: Debuff) {
n371=      var %ffrsptype hinder
n372=      var %ffrspgfxshape 26
n373=      var %ffrspbatmsg 0; None
n374=      if ( $read(WhiteSpell.txt,2) = Effect: Darkness) {
n375=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n376=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname DIM  $+ }
n377=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var %ffrspgfxclr 12 (Pale Cyan) | var -n %ffrspname GLOW }
n378=        }
n379=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n380=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname BLUR }
n381=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname VOID }
n382=        }
n383=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n384=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SAND }
n385=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname TAR  $+ }
n386=        }
n387=        else {
n388=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname VEIL }
n389=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname COVR }
n390=        }
n391=      }
n392=      elseif ( $read(WhiteSpell.txt,2) = Effect: Stun) {
n393=        if (time !isin $read(WhiteSpell.txt,w,*element*)) {
n394=          var %ffrspbatmsg 13 (Attack halted)
n395=          var %ffrspcommand set % $+ ffrspunmsg13 [Assigned]
n396=        }
n397=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n398=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HOLD }
n399=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname BIND }
n400=        }
n401=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n402=          var %ffrspbatmsg 30 (Time stopped)
n403=          var %ffrspcommand set % $+ ffrspunmsg30 [Assigned]
n404=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname PAUS }
n405=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname STOP }
n406=        }
n407=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n408=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SNAR }
n409=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname VINE }
n410=        }
n411=        else {
n412=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname PIN  $+ }
n413=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HALT }
n414=        }
n415=      }
n416=      elseif ( $read(WhiteSpell.txt,2) = Effect: Sleep) {
n417=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n418=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HYP  $+ }
n419=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname LULL }
n420=        }
n421=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n422=          var %ffrspbatmsg 30 (Time stopped)
n423=          var %ffrspcommand set % $+ ffrspunmsg30 [Assigned]
n424=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SKIP }
n425=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname JUMP }
n426=        }
n427=        elseif ( $read(WhiteSpell.txt,3) = Element: Fire) {
n428=          var %ffrsptype blank
n429=          if (%ffrwmtier > 1) { var %ffrspname CMP $+ %ffrwmtier }
n430=          var -n %ffrspname CAMP
n431=          var %ffrspgfxshape 16
n432=          var %ffrspgfxclr 7 (Red)
n433=        }
n434=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n435=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname BED  $+ }
n436=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname WAFT }
n437=        }
n438=        else {
n439=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname NAP  $+ }
n440=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SLEP }
n441=        }
n442=      }
n443=      elseif ( $read(WhiteSpell.txt,2) = Effect: Mute) {
n444=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n445=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname MUFF }
n446=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname MUTE }
n447=        }
n448=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n449=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname ECHO }
n450=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname VACU }
n451=        }
n452=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n453=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname MUDL }
n454=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname FUNL }
n455=        }
n456=        else {
n457=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HUSH }
n458=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname QIET }
n459=        }
n460=      }
n461=      elseif ( $read(WhiteSpell.txt,2) = Effect: Confuse) {
n462=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n463=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname TRIK }
n464=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname CONF }
n465=        }
n466=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n467=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname FLIP }
n468=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SPIN }
n469=        }
n470=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n471=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SEED }
n472=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname MAZE }
n473=        }
n474=        else {
n475=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname TURN }
n476=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HAVC }
n477=        }
n478=      }
n479=      elseif ( $read(WhiteSpell.txt,2) = Effect: Slow) {
n480=        var %ffrspgfxshape 14
n481=        var %ffrspbatmsg 11 (Lost intelligence)
n482=        var %ffrspcommand set % $+ ffrspunmsg11 [Assigned]
n483=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n484=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HMPR }
n485=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname DULL }
n486=        }
n487=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n488=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname AGE  $+ }
n489=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname BACK }
n490=        }
n491=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n492=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SAP }
n493=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname BOG  $+ }
n494=        }
n495=        else {
n496=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname TIRE }
n497=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SLOW }
n498=        }
n499=      }
n500=      elseif ( $read(WhiteSpell.txt,2) = Effect: Fear) {
n501=        var %ffrsptype fight
n502=        var %ffrspgfxshape 14
n503=        var %ffrspbatmsg 15 (Became terrified)
n504=        var %ffrspcommand set % $+ ffrspunmsg15 [Assigned]
n505=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n506=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { 
n507=            if (%ffrwmtier > 1) { var %ffrspname ANX $+ %ffrwmtier }
n508=            else { var -n %ffrspname ANX  $+ }
n509=          }
n510=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n511=            if (%ffrwmtier > 1) { var %ffrspname FER $+ %ffrwmtier }
n512=            else { var -n %ffrspname FEAR }
n513=          }
n514=        }
n515=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n516=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) {
n517=            if (%ffrwmtier > 1) { var %ffrspname SIZ $+ %ffrwmtier }
n518=            else { var -n %ffrspname SIZE }
n519=          }
n520=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n521=            if (%ffrwmtier > 1) { var %ffrspname GRO $+ %ffrwmtier }
n522=            else { var -n %ffrspname GROW }
n523=          }
n524=        }
n525=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n526=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) {
n527=            if (%ffrwmtier > 1) { var %ffrspname TER $+ %ffrwmtier }
n528=            else { var -n %ffrspname TERR }
n529=          }
n530=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n531=            if (%ffrwmtier > 1) { var %ffrspname GAP $+ %ffrwmtier }
n532=            else { var -n %ffrspname GAPE }
n533=          }
n534=        }
n535=        else {
n536=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) {
n537=            if (%ffrwmtier > 1) { var %ffrspname PSH $+ %ffrwmtier }
n538=            else { var -n %ffrspname PUSH }
n539=          }
n540=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n541=            if (%ffrwmtier > 1) { var %ffrspname RPL $+ %ffrwmtier }
n542=            else { var -n %ffrspname REPL }
n543=          }
n544=        }
n545=      }
n546=      elseif ( $read(WhiteSpell.txt,2) = Effect: Locked) {
n547=        var %ffrsptype fight
n548=        var %ffrspgfxshape 14
n549=        var %ffrspbatmsg 5 (Easy to hit)
n550=        var %ffrspcommand set % $+ ffrspunmsg5 [Assigned]
n551=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n552=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) {
n553=            if (%ffrwmtier > 1) { var %ffrspname CAG $+ %ffrwmtier }
n554=            else { var -n %ffrspname CAGE }
n555=          }
n556=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n557=            if (%ffrwmtier > 1) { var %ffrspname NET $+ %ffrwmtier }
n558=            else { var -n %ffrspname NET  $+ }
n559=          }
n560=        }
n561=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n562=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) {
n563=            if (%ffrwmtier > 1) { var %ffrspname WEL $+ %ffrwmtier }
n564=            else { var -n %ffrspname WELL }
n565=          }
n566=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n567=            if (%ffrwmtier > 1) { var %ffrspname GRV $+ %ffrwmtier }
n568=            else { var -n %ffrspname GRAV }
n569=          }
n570=        }
n571=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n572=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) {
n573=            if (%ffrwmtier > 1) { var %ffrspname PIT $+ %ffrwmtier }
n574=            else { var -n %ffrspname PIT  $+ }
n575=          }
n576=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n577=            if (%ffrwmtier > 1) { var %ffrspname OUB $+ %ffrwmtier }
n578=            else { var -n %ffrspname OUBL }
n579=          }
n580=        }
n581=        else {
n582=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) {
n583=            if (%ffrwmtier > 1) { var %ffrspname LOK $+ %ffrwmtier }
n584=            else { var -n %ffrspname LOCK }
n585=          }
n586=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) {
n587=            if (%ffrwmtier > 1) { var %ffrspname SEL $+ %ffrwmtier }
n588=            else { var -n %ffrspname SEAL }
n589=          }
n590=        }
n591=      }
n592=      elseif ( $read(WhiteSpell.txt,2) = Effect: Stripped) {
n593=        var %ffrspgfxshape 14
n594=        var %ffrspbatmsg 29 (Defenseless)
n595=        var %ffrspcommand set % $+ ffrspunmsg29 [Assigned]
n596=        if ( $read(WhiteSpell.txt,3) = Element: Status) {
n597=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname NFIL }
n598=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname NEUT }
n599=        }
n600=        elseif ( $read(WhiteSpell.txt,3) = Element: Time) {
n601=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SET  $+ }
n602=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname NULL }
n603=        }
n604=        elseif ( $read(WhiteSpell.txt,3) = Element: Earth) {
n605=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname PIRC }
n606=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname DRAN }
n607=        }
n608=        else {
n609=          if ( $read(WhiteSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname XFER }
n610=          elseif ( $read(WhiteSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname XPOS }
n611=        }
n612=      }
n613=      elseif ($read(WhiteSpell.txt,w,*word*)) {
n614=        var %ffrsptype word
n615=        var %ffrspgfxshape 17
n616=        if (time isin $read(WhiteSpell.txt,w,*element*)) && (($read(WhiteSpell.txt,w,*stun*)) || ($read(WhiteSpell.txt,w,*nap*))) {
n617=          var %ffrspbatmsg 30 (Time stopped)
n618=          var %ffrspcommand set % $+ ffrspunmsg30 [Assigned]
n619=        }
n620=        if ( $read(WhiteSpell.txt,2) = Effect: Power Word "Pacify") {
n621=          var -n %ffrspname FROG
n622=          var %ffrspgfxclr 10 (Bright Green)
n623=          var %ffrspbatmsg 47 (Stopped)
n624=          var %ffrspcommand set % $+ ffrspunmsg47 [Assigned]
n625=        }
n626=        elseif ( $read(WhiteSpell.txt,2) = Effect: Power Word "Preserve") {
n627=          var %ffrsptype blank
n628=          var -n %ffrspname CAST
n629=          var %ffrspgfxclr 1 (White)
n630=        }
n631=        elseif ( $read(WhiteSpell.txt,2) = Effect: Power Word "Blind") { var -n %ffrspname BLND }
n632=        elseif ( $read(WhiteSpell.txt,2) = Effect: Power Word "Stun") { var -n %ffrspname STUN }
n633=        elseif ( $read(WhiteSpell.txt,2) = Effect: Power Word "Naptime") { var -n %ffrspname ZZZZ }
n634=        elseif ( $read(WhiteSpell.txt,2) = Effect: Power Word "Silence") { var -n %ffrspname JINX }
n635=        elseif ( $read(WhiteSpell.txt,2) = Effect: Power Word "Betray") { var -n %ffrspname FOOL }
n636=      }
n637=      else { echo 13 -s Debug: Unidentified Hindrance ( $+ $read(WhiteSpell.txt,2) $+ ) | write -a1 $+ %ffrspline SpellTable.txt WHITE DEBUFF }
n638=    }
n639=    else { echo 13 -s Debug: Unidentified Effect ( $+ $read(WhiteSpell.txt,1) $+ ) |  write -al $+ %ffrspline SpellTable.txt WHITE SPELL }
n640=  }
n641=  elseif (%ffrspmagic = black) {
n642=    /bmag
n643=    if (status isin $read(BlackSpell.txt,w,*element*)) || (kinetic isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 6 (Magenta) }
n644=    elseif (poison isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 4 (Purple) }
n645=    elseif (stone isin $read(BlackSpell.txt,w,*element*)) || (storm isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 1 (White) }
n646=    elseif (time isin $read(BlackSpell.txt,w,*element*)) || (plasma isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 11 (Dark Green) }
n647=    elseif (death isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 14 (Grey) }
n648=    elseif (antipode isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 2 (Light Blue) }
n649=    elseif (fire isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 7 (Red) }
n650=    elseif (ice isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 13 (Bright Cyan) }
n651=    elseif (lit isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 3 (Dark Blue) }
n652=    elseif (earth isin $read(BlackSpell.txt,w,*element*)) || (magma isin $read(BlackSpell.txt,w,*element*)) { var %ffrspgfxclr 8 (Orange) }
n653=    else { var %ffrspgfxclr 9 (Yellow) }
n654=    if ( $read(BlackSpell.txt,1) = Type: Damage) {
n655=      var %ffrsptype fight
n656=      var %ffrspgfxshape 20
n657=      if ($read(BlackSpell.txt,w,*auto*)) {
n658=        var %ffrspbatmsg 43 (Critical hit!!)
n659=        var %ffrspcommand set % $+ ffrspunmsg43 [Assigned]
n660=      }
n661=      else { var %ffrspbatmsg 0; None }
n662=      if ($read(BlackSpell.txt,w,*single*)) { inc %ffrspgfxshape 1 }
n663=      if ( $read(BlackSpell.txt,2) = Element: None) {
n664=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n665=          if (%ffrbmtier > 1) { var %ffrspname GYS $+ %ffrbmtier }
n666=          else { var -n %ffrspname GYSR }
n667=        }
n668=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n669=          if (%ffrbmtier = 4) { var -n %ffrspname NUKE }
n670=          elseif (%ffrbmtier > 1) { var %ffrspname BOM $+ %ffrbmtier }
n671=          else { var -n %ffrspname BOMB }
n672=        }
n673=      }
n674=      elseif ( $read(BlackSpell.txt,2) = Element: Fire) {
n675=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n676=          if (%ffrbmtier > 1) { var %ffrspname BRN $+ %ffrbmtier }
n677=          else { var -n %ffrspname BURN }
n678=        }
n679=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n680=          if (%ffrbmtier = 4) { var -n %ffrspname BOIL }
n681=          elseif (%ffrbmtier > 1) { var %ffrspname FIR $+ %ffrbmtier }
n682=          else { var -n %ffrspname FIRE }
n683=        }
n684=      }
n685=      elseif ( $read(BlackSpell.txt,2) = Element: Ice) {
n686=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n687=          if (%ffrbmtier > 1) { var %ffrspname SNO $+ %ffrbmtier }
n688=          else { var -n %ffrspname SNOW }
n689=        }
n690=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n691=          if (%ffrbmtier = 4) { var -n %ffrspname HAIL }
n692=          elseif (%ffrbmtier > 1) { var %ffrspname ICE $+ %ffrbmtier }
n693=          else { var -n %ffrspname ICE }
n694=        }
n695=      }
n696=      elseif ( $read(BlackSpell.txt,2) = Element: Lit) {
n697=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n698=          var %ffrspgfxshape 18
n699=          if (%ffrbmtier > 1) { var %ffrspname ION $+ %ffrbmtier }
n700=          else { var -n %ffrspname ION }
n701=        }
n702=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n703=          var %ffrspgfxshape 19
n704=          if (%ffrbmtier = 4) { var -n %ffrspname VOLT }
n705=          elseif (%ffrbmtier > 1) { var %ffrspname LIT $+ %ffrbmtier }
n706=          else { var -n %ffrspname LIT }
n707=        }
n708=      }
n709=      elseif ( $read(BlackSpell.txt,2) = Element: Poison) {
n710=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n711=          if (%ffrbmtier > 1) { var %ffrspname VNM $+ %ffrbmtier }
n712=          else { var -n %ffrspname VENM }
n713=        }
n714=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n715=          if (%ffrbmtier = 4) { var -n %ffrspname BILE }
n716=          elseif (%ffrbmtier > 1) { var %ffrspname FUM $+ %ffrbmtier }
n717=          else { var -n %ffrspname FUME }
n718=        }
n719=      }
n720=      elseif ( $read(BlackSpell.txt,2) = Element: Death) {
n721=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n722=          if (%ffrbmtier > 1) { var %ffrspname ROT $+ %ffrbmtier }
n723=          else { var -n %ffrspname ROT  $+ }
n724=        }
n725=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n726=          if (%ffrbmtier = 4) { var -n %ffrspname DOOM }
n727=          elseif (%ffrbmtier > 1) { var %ffrspname NEC $+ %ffrbmtier }
n728=          else { var -n %ffrspname NECR }
n729=        }
n730=      }
n731=      elseif ( $read(BlackSpell.txt,2) = Element: Antipode) {
n732=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n733=          if (%ffrbmtier > 1) { var %ffrspname HYD $+ %ffrbmtier }
n734=          else { var -n %ffrspname HYDR }
n735=        }
n736=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n737=          if (%ffrbmtier = 4) { var -n %ffrspname ATOM }
n738=          elseif (%ffrbmtier > 1) { var %ffrspname POD $+ %ffrbmtier }
n739=          else { var -n %ffrspname PODE }
n740=        }
n741=      }
n742=      elseif ( $read(BlackSpell.txt,2) = Element: Plasma) {
n743=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n744=          if (%ffrbmtier > 1) { var %ffrspname NRG $+ %ffrbmtier }
n745=          else { var -n %ffrspname ENRG }
n746=        }
n747=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n748=          if (%ffrbmtier = 4) { var -n %ffrspname NOVA }
n749=          elseif (%ffrbmtier > 1) { var %ffrspname PLZ $+ %ffrbmtier }
n750=          else { var -n %ffrspname PLAZ }
n751=        }
n752=      }
n753=      elseif ( $read(BlackSpell.txt,2) = Element: Storm) {
n754=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n755=          if (%ffrbmtier > 1) { var %ffrspname ARC $+ %ffrbmtier }
n756=          else { var -n %ffrspname ARC  $+ }
n757=        }
n758=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n759=          if (%ffrbmtier = 4) { var -n %ffrspname GALE }
n760=          elseif (%ffrbmtier > 1) { var %ffrspname AIR $+ %ffrbmtier }
n761=          else { var -n %ffrspname AIR  $+ }
n762=        }
n763=      }
n764=      elseif ( $read(BlackSpell.txt,2) = Element: Magma) {
n765=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n766=          if (%ffrbmtier > 1) { var %ffrspname ASH $+ %ffrbmtier }
n767=          else { var -n %ffrspname ASH  $+ }
n768=        }
n769=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n770=          if (%ffrbmtier = 4) { var -n %ffrspname RUPT }
n771=          elseif (%ffrbmtier > 1) { var %ffrspname LAV $+ %ffrbmtier }
n772=          else { var -n %ffrspname LAVA }
n773=        }
n774=      }
n775=      elseif ( $read(BlackSpell.txt,2) = Element: Kinetic) {
n776=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { 
n777=          if (%ffrbmtier > 1) { var %ffrspname KIN $+ %ffrbmtier }
n778=          else { var -n %ffrspname KIN  $+ }
n779=        }
n780=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) {
n781=          if (%ffrbmtier = 4) { var -n %ffrspname YANK }
n782=          elseif (%ffrbmtier > 1) { var %ffrspname WAV $+ %ffrbmtier }
n783=          else { var -n %ffrspname WAVE }
n784=        }
n785=      }
n786=      else { echo 11 -s Debug: Unidentified Damage ( $+ $read(BlackSpell.txt,2) $+ ) | write -a1 $+ %ffrspline SpellTable.txt BLACK DAMAGE }
n787=    }
n788=    elseif ( $read(BlackSpell.txt,1) = Type: Insta-Kill) {
n789=      var %ffrsptype hinder
n790=      var %ffrspgfxshape 22
n791=      if ($read(BlackSpell.txt,w,*party*)) { inc %ffrspgfxshape 1 }
n792=      if ( $read(BlackSpell.txt,2) = Element: None) {
n793=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var -n %ffrspname FALL }
n794=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var -n %ffrspname END  $+ }
n795=      }
n796=      elseif ( $read(BlackSpell.txt,2) = Element: Fire) {
n797=        var %ffrspbatmsg 21 (Erased)
n798=        var %ffrspcommand set % $+ ffrspunmsg21 [Assigned]
n799=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var -n %ffrspname MELT }
n800=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var -n %ffrspname EVAP }
n801=      }
n802=      elseif ( $read(BlackSpell.txt,2) = Element: Ice) {
n803=        var %ffrspbatmsg 76 (Frozen)
n804=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var %ffrspgfxshape 18 | var -n %ffrspname CRYO }
n805=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var %ffrspgfxshape 19 | var -n %ffrspname FREZ }
n806=      }
n807=      elseif ( $read(BlackSpell.txt,2) = Element: Lit) {
n808=        var %ffrspbatmsg 31 (Exile to 4th dimension)
n809=        var %ffrspcommand set % $+ ffrspunmsg31 [Assigned]
n810=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var -n %ffrspname SMIT }
n811=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var -n %ffrspname BZZT }
n812=      }
n813=      elseif ( $read(BlackSpell.txt,2) = Element: Smoke) {
n814=        var %ffrspgfxclr 4 (Purple)
n815=        var %ffrspbatmsg 77 (Poison smoke)
n816=        var %ffrspcommand set % $+ ffrspunmsg77 [Assigned]
n817=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var -n %ffrspname CHOK }
n818=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var -n %ffrspname BANE }
n819=      }
n820=      elseif ( $read(BlackSpell.txt,2) = Element: Erase) {
n821=        var %ffrspgfxclr 14 (Grey)
n822=        var %ffrspbatmsg 21 (Erased)
n823=        var %ffrspcommand set % $+ ffrspunmsg21 [Assigned]
n824=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var -n %ffrspname RUB  $+ }
n825=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var -n %ffrspname ERAD }
n826=      }
n827=      elseif ( $read(BlackSpell.txt,2) = Element: Exile) {
n828=        var %ffrspgfxclr 11 (Dark Green)
n829=        var %ffrspbatmsg 31 (Exile to 4th dimension)
n830=        var %ffrspcommand set % $+ ffrspunmsg31 [Assigned]
n831=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var -n %ffrspname BYE! }
n832=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var -n %ffrspname ZAP! }
n833=      }
n834=      elseif ( $read(BlackSpell.txt,2) = Element: Break) {
n835=        var %ffrspgfxclr 1 (White)
n836=        var %ffrspbatmsg 0; None
n837=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var %ffrspgfxshape 18 | var -n %ffrspname BRAK }
n838=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var %ffrspgfxshape 19 | var -n %ffrspname WREK }
n839=      }
n840=      elseif ( $read(BlackSpell.txt,2) = Element: Crack) {
n841=        var %ffrspgfxclr 8 (Orange)
n842=        var %ffrspbatmsg 22 (Fell into crack)
n843=        var %ffrspcommand set % $+ ffrspunmsg22 [Assigned]
n844=        if ( $read(BlackSpell.txt,3) = Target: Single Enemy) { var -n %ffrspname SWAL }
n845=        elseif ( $read(BlackSpell.txt,3) = Target: Enemy Party) { var -n %ffrspname QAKE }
n846=      }
n847=      else { echo 11 -s Debug: Unidentified Insta-Kill ( $+ $read(BlackSpell.txt,2) $+ ) | write -a1 $+ %ffrspline SpellTable.txt BLACK KILL }
n848=    }
n849=    elseif ( $read(BlackSpell.txt,1) = Type: Buff) {
n850=      var %ffrsptype help
n851=      var %ffrspgfxshape 12
n852=      if ($read(BlackSpell.txt,w,*allies*)) { inc %ffrspgfxshape 1 }
n853=      if ( $read(BlackSpell.txt,2) = Effect: Attack Up) {
n854=        var %ffrspgfxclr 7 (Red)
n855=        var %ffrspbatmsg 10 (Weapons stronger)
n856=        var %ffrspcommand set % $+ ffrspunmsg10 [Assigned]
n857=        if ( $read(BlackSpell.txt,3) = Target: Single Ally) {
n858=          if (%ffrbmtier > 1) { var %ffrspname TMP $+ %ffrbmtier }
n859=          else { var -n %ffrspname TMPR }
n860=        }
n861=        elseif ( $read(BlackSpell.txt,3) = Target: Caster) {
n862=          var %ffrspgfxclr 2 (Light Blue)
n863=          if (%ffrbmtier > 1) { var %ffrspname SAB $+ %ffrbmtier }
n864=          else { var -n %ffrspname SABR }
n865=        }
n866=        elseif ( $read(BlackSpell.txt,3) = Target: All Allies) {
n867=          if (%ffrbmtier > 1) { var %ffrspname RAL $+ %ffrbmtier }
n868=          else { var -n %ffrspname RALY }
n869=        }
n870=        else { echo 11 -s Debug: Invalid TMPR Target | write -al $+ %ffrspline SpellTable.txt BLACK ATTACK UP }
n871=      }
n872=      elseif ( $read(BlackSpell.txt,2) = Effect: Double Hits) {
n873=        var %ffrsptype blank
n874=        var %ffrspgfxclr 6 (Magenta)
n875=        if (caster !isin $read(BlackSpell.txt,w,*target*)) {
n876=          var %ffrspbatmsg 18 (Quick shot)
n877=          var %ffrspcommand set % $+ ffrspunmsg18 [Assigned]
n878=        }
n879=        if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var %ffrspgfxclr 11 (Dark Green) | var -n %ffrspname FAST }
n880=        elseif ( $read(BlackSpell.txt,3) = Target: Caster) { unset %ffrspbatmsg | var -n %ffrspname BSRK }
n881=        elseif ( $read(BlackSpell.txt,3) = Target: All Allies) { var -n %ffrspname FURY } 
n882=        else { echo 11 -s Debug: Invalid FAST Target | write -al $+ %ffrspline SpellTable.txt BLACK DOUBLE HITS }
n883=      }
n884=      elseif ( $read(BlackSpell.txt,2) = Effect: Absorb Up) {
n885=        var %ffrspgfxclr 13 (Bright Cyan)
n886=        var %ffrspbatmsg 2 (Armor up)
n887=        var %ffrspcommand set % $+ ffrspunmsg2 [Assigned]
n888=        if (%ffrbmtier > 1) { var %ffrspname ARM $+ %ffrbmtier }
n889=        else { var -n %ffrspname ARM  $+ }
n890=      }
n891=      elseif ( $read(BlackSpell.txt,2) = Effect: Evade Up) {
n892=        var %ffrspgfxclr 4 (Purple)
n893=        var %ffrspbatmsg 3 (Easy to dodge)
n894=        var %ffrspcommand set % $+ ffrspunmsg3 [Assigned]
n895=        if (%ffrbmtier > 1) { var %ffrspname HID $+ %ffrbmtier }
n896=        else { var -n %ffrspname HIDE }
n897=      }
n898=      elseif ($read(BlackSpell.txt,w,*resist*)) {
n899=        var %ffrsptype blank
n900=        ; Make some complex slot assignment system here
n901=        if ( $read(BlackSpell.txt,2) = Effect: Resist Status) {
n902=          var %ffrspgfxclr 6 (Magenta)
n903=          var %ffrspbatmsg Defend weak
n904=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname BRAC }
n905=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname GRAC }
n906=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK STATUS RESIST }
n907=        }
n908=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Poison/Stone) {
n909=          var %ffrspgfxclr 4 (Purple)
n910=          var %ffrspbatmsg Defend smoke
n911=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname VCIN }
n912=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname IMUN }
n913=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK POISON RESIST }
n914=        }
n915=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Time) {
n916=          var %ffrspgfxclr 11 (Dark Green)
n917=          var %ffrspbatmsg Defend exile
n918=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname XCEP }
n919=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname PHAS }
n920=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK TIME RESIST }
n921=        }
n922=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Death) {
n923=          var %ffrspgfxclr 14 (Grey)
n924=          var %ffrspbatmsg Defend evil
n925=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname SAFE }
n926=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname AURA }
n927=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK DEATH RESIST }
n928=        }
n929=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Fire) {
n930=          var %ffrspgfxclr 7 (Red)
n931=          var %ffrspbatmsg Defend fire
n932=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname DOUS }
n933=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname COOL }
n934=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK FIRE RESIST }
n935=        }
n936=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Ice) {
n937=          var %ffrspgfxclr 13 (Bright Cyan)
n938=          var %ffrspbatmsg Defend cold
n939=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname COAT }
n940=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname WARM }
n941=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK ICE RESIST }
n942=        }
n943=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Lit) {
n944=          var %ffrspgfxclr 3 (Dark Blue)
n945=          var %ffrspbatmsg Defend lightning
n946=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname OHM  }
n947=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname SURG }
n948=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK LIT RESIST }
n949=        }
n950=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Earth) {
n951=          var %ffrspgfxclr 10 (Bright Green)
n952=          var %ffrspbatmsg Defend crack
n953=          if ( $read(BlackSpell.txt,3) = Target: Single Ally) { var -n %ffrspname LIFT }
n954=          elseif ( $read(BlackSpell.txt,3) = Target: Caster) { var -n %ffrspname FLOT }
n955=          else { echo 11 -s Debug: Invalid Resist Target | write -al $+ %ffrspline SpellTable.txt BLACK EARTH RESIST }
n956=        }
n957=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Dragon) { var %ffrspgfxclr 2 (Light Blue) | var %ffrspbatmsg Defend spell | var -n %ffrspname PRSM }
n958=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Magic) { var %ffrspgfxclr 6 (Magenta) | var %ffrspbatmsg Defend magic | var -n %ffrspname PROT }
n959=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist Decay) { var %ffrspgfxclr 4 (Purple) | var %ffrspbatmsg Defend aging | var -n %ffrspname STAG }
n960=        elseif ( $read(BlackSpell.txt,2) = Effect: Resist All) {
n961=          var %ffrspbatmsg Defend all
n962=          var -n %ffrspname BARR
n963=        }
n964=      }
n965=      elseif ( $read(BlackSpell.txt,2) = Effect: Regenerate) {
n966=        var %ffrspgfxshape 15
n967=        if (%ffrbmtier < 4) {
n968=          var %ffrspbatmsg 1 (HP up!)
n969=          var %ffrspcommand set % $+ ffrspunmsg1 [Assigned]
n970=        }
n971=        else {
n972=          var %ffrspbatmsg 24 (HP max!)
n973=          var %ffrspcommand set % $+ ffrspunmsg24 [Assigned]
n974=        }
n975=        if (%ffrbmtier = 1) { var %ffrspgfxclr 14 (Grey) | var -n %ffrspname REGN }
n976=        elseif (%ffrbmtier < 4) {
n977=          if (%ffrbmtier = 2) { var %ffrspgfxclr 8 (Orange) }
n978=          elseif (%ffrbmtier = 3) { var %ffrspgfxclr 11 (Dark Green) }
n979=          var -n %ffrspname RGN $+ %ffrbmtier
n980=        }
n981=        else { var %ffrsptype blank | var %ffrspgfxclr 9 (Yellow) | var -n %ffrspname RGN4 }
n982=      }
n983=      else { echo 11 -s Debug: Unidentified Buff ( $+ $read(BlackSpell.txt,2) $+ ) | write -a1 $+ %ffrspline SpellTable.txt BLACK BUFF }
n984=    }
n985=    elseif ( $read(BlackSpell.txt,1) = Type: Debuff) {
n986=      var %ffrsptype hinder
n987=      var %ffrspgfxshape 26
n988=      if (poison isin $read(BlackSpell.txt,w,*effect*)) && (fire !isin $read(BlackSpell.txt,w,*element*)) {
n989=        var %ffrspbatmsg 77 (Poison smoke)
n990=        var %ffrspcommand set % $+ ffrspunmsg77 [Assigned]
n991=      }
n992=      elseif (poison isin $read(BlackSpell.txt,w,*effect*)) && (fire isin $read(BlackSpell.txt,w,*element*)) { unset %ffrspbatmsg }
n993=      else { var %ffrspbatmsg 0; None }
n994=      if ( $read(BlackSpell.txt,2) = Effect: Dark) {
n995=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n996=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname DIM  $+ }
n997=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n998=            var %ffrspgfxclr 14 (Grey)
n999=            var -n %ffrspname DARK
n1000=          }
n1001=        }
n1002=        elseif ( $read(BlackSpell.txt,3) = Element: Poison) {
n1003=          var %ffrspgfxclr 1 (White)
n1004=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SMOG }
n1005=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HAZE }
n1006=        }
n1007=        elseif ( $read(BlackSpell.txt,3) = Element: Time) {
n1008=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname BLUR }
n1009=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname VOID }
n1010=        } 
n1011=        elseif ( $read(BlackSpell.txt,3) = Element: Fire) {
n1012=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname PEPR }
n1013=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HUMD }
n1014=        }
n1015=        elseif ( $read(BlackSpell.txt,3) = Element: Earth) {
n1016=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SAND }
n1017=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname TAR  $+ }
n1018=        }
n1019=        else {
n1020=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname VEIL }
n1021=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname COVR }
n1022=        }
n1023=      }
n1024=      elseif ( $read(BlackSpell.txt,2) = Effect: Dark & Poison ) {
n1025=        var %ffrsptype toxin
n1026=        if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SMOG }
n1027=        elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HAZE }
n1028=      }
n1029=      elseif ( $read(BlackSpell.txt,2) = Effect: Stun) {
n1030=        if (time !isin $read(BlackSpell.txt,w,*element*)) && (Ice !isincs $read(BlackSpell.txt,w,*element*)) {
n1031=          var %ffrspbatmsg 13 (Attack halted)
n1032=          var %ffrspcommand set % $+ ffrspunmsg13 [Assigned]
n1033=        }
n1034=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n1035=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HOLD }
n1036=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname BIND }
n1037=        }
n1038=        elseif ( $read(BlackSpell.txt,3) = Element: Time) {
n1039=          var %ffrspbatmsg 30 (Time stopped)
n1040=          var %ffrspcommand set % $+ ffrspunmsg30 [Assigned]
n1041=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname PAUS }
n1042=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname STOP }
n1043=        }
n1044=        elseif ( $read(BlackSpell.txt,3) = Element: Ice) {
n1045=          var %ffrspbatmsg 76 (Frozen)
n1046=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname ZERO }
n1047=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SQAL }
n1048=        }
n1049=        elseif ( $read(BlackSpell.txt,3) = Element: Lit) {
n1050=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname FRY  $+ }
n1051=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname EMP  $+ }
n1052=        }
n1053=        else {
n1054=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname PIN  $+ }
n1055=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HALT }
n1056=        }
n1057=      }
n1058=      elseif ( $read(BlackSpell.txt,2) = Effect: Stun & Poison ) {
n1059=        var %ffrsptype toxin
n1060=        if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SICK }
n1061=        elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname RIG  $+ }
n1062=      }
n1063=      elseif ( $read(BlackSpell.txt,2) = Effect: Sleep) {
n1064=        ; echo 11 -s Debug: Sleep $read(BlackSpell.txt,3)
n1065=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n1066=          ; echo 11 -s Debug: Sleep $read(BlackSpell.txt,4)
n1067=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HYP  $+ }
n1068=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname LULL }
n1069=        }
n1070=        elseif ( $read(BlackSpell.txt,3) = Element: Time) {
n1071=          var %ffrspbatmsg 30 (Time stopped)
n1072=          var %ffrspcommand set % $+ ffrspunmsg30 [Assigned]
n1073=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SKIP }
n1074=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname JUMP }
n1075=        }
n1076=        elseif ( $read(BlackSpell.txt,3) = Element: Death) {
n1077=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname PASS }
n1078=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname DEPR }
n1079=        }
n1080=        elseif ( $read(BlackSpell.txt,3) = Element: Fire) {
n1081=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname COZY }
n1082=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SOOT }
n1083=        }
n1084=        elseif ( $read(BlackSpell.txt,3) = Element: Ice) {
n1085=          var %ffrspbatmsg 76 (Frozen)
n1086=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname CHIL }
n1087=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HIB  $+ }
n1088=        }
n1089=        else {
n1090=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname NAP  $+ }
n1091=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SLEP }
n1092=        }
n1093=      }
n1094=      elseif ( $read(BlackSpell.txt,2) = Effect: Sleep & Poison ) {
n1095=        var %ffrsptype toxin
n1096=        if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname K.O. }
n1097=        elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname GAS  $+ }
n1098=      }
n1099=      elseif ( $read(BlackSpell.txt,2) = Effect: Mute) {
n1100=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n1101=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname MUFF }
n1102=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname MUTE }
n1103=        }
n1104=        elseif ( $read(BlackSpell.txt,3) = Element: Poison) {
n1105=          var %ffrspgfxclr 1 (White)
n1106=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname COGH }
n1107=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname FLU  $+ }
n1108=        }
n1109=        elseif ( $read(BlackSpell.txt,3) = Element: Time) {
n1110=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname ECHO }
n1111=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname VACU }
n1112=        }
n1113=        elseif ( $read(BlackSpell.txt,3) = Element: Ice) {
n1114=          var %ffrspbatmsg 76 (Frozen)
n1115=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname BREZ }
n1116=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HOWL }
n1117=        }
n1118=        else {
n1119=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HUSH }
n1120=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname QIET }
n1121=        }
n1122=      }
n1123=      elseif ( $read(BlackSpell.txt,2) = Effect: Mute & Poison ) {
n1124=        var %ffrsptype toxin
n1125=        if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname COGH }
n1126=        elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname FLU  $+ }
n1127=      }
n1128=      elseif ( $read(BlackSpell.txt,2) = Effect: Confuse) {
n1129=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n1130=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname TRIK }
n1131=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname CONF }
n1132=        }
n1133=        elseif ( $read(BlackSpell.txt,3) = Element: Time) {
n1134=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname FLIP }
n1135=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SPIN }
n1136=        }
n1137=        elseif ( $read(BlackSpell.txt,3) = Element: Fire) {
n1138=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname TORC | unset %ffrspbatmsg }
n1139=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname PYRO | unset %ffrspbatmsg }
n1140=        }
n1141=        elseif ( $read(BlackSpell.txt,3) = Element: Lit) {
n1142=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname RAY  $+ }
n1143=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname RAVE }
n1144=        }
n1145=        elseif ( $read(BlackSpell.txt,3) = Element: Earth) {
n1146=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SEED }
n1147=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname MAZE }
n1148=        }
n1149=        else {
n1150=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname TURN }
n1151=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HAVC }
n1152=        }
n1153=      }
n1154=      elseif ( $read(BlackSpell.txt,2) = Effect: Confuse & Poison ) {
n1155=        var %ffrsptype toxin
n1156=        if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname POX }
n1157=        elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname VIR  $+ }
n1158=      }
n1159=      elseif ( $read(BlackSpell.txt,2) = Effect: Dehab ) {
n1160=        var %ffrsptype toxin
n1161=        if (time isin $read(BlackSpell.txt,w,*element*)) {
n1162=          var %ffrspbatmsg 30 (Time stopped)
n1163=          var %ffrspcommand set % $+ ffrspunmsg30 [Assigned]
n1164=        }
n1165=        elseif (Ice isincs $read(BlackSpell.txt,w,*element*)) { var %ffrspbatmsg 76 (Frozen) }
n1166=        if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname CURS }
n1167=        elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname HEX  $+ }
n1168=      }
n1169=      elseif ( $read(BlackSpell.txt,2) = Effect: Slow) {
n1170=        var %ffrspgfxshape 14
n1171=        var %ffrspbatmsg 11 (Lost intelligence)
n1172=        var %ffrspcommand set % $+ ffrspunmsg11 [Assigned]
n1173=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n1174=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname HMPR }
n1175=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname DULL }
n1176=        }
n1177=        elseif ( $read(BlackSpell.txt,3) = Element: Poison) {
n1178=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname NUMB }
n1179=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var %ffrspgfxclr 1 (White) | var -n %ffrspname MIST }
n1180=        }
n1181=        elseif ( $read(BlackSpell.txt,3) = Element: Time) {
n1182=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname AGE  $+ }
n1183=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname BACK }
n1184=        }
n1185=        elseif ( $read(BlackSpell.txt,3) = Element: Death) {
n1186=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname ATRO }
n1187=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname MIAS }
n1188=        }
n1189=        elseif ( $read(BlackSpell.txt,3) = Element: Ice) {
n1190=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname BRRR }
n1191=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname COLD }
n1192=        }
n1193=        else {
n1194=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname TIRE }
n1195=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname SLOW }
n1196=        }
n1197=      }
n1198=      elseif ( $read(BlackSpell.txt,2) = Effect: Fear) {
n1199=        var %ffrsptype fight
n1200=        var %ffrspgfxshape 14
n1201=        var %ffrspbatmsg 15 (Became terrified)
n1202=        var %ffrspcommand set % $+ ffrspunmsg15 [Assigned]
n1203=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n1204=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1205=            if (%ffrbmtier > 1) { var %ffrspname ANX $+ %ffrbmtier }
n1206=            else { var -n %ffrspname ANX  $+ }
n1207=          }
n1208=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1209=            if (%ffrbmtier > 1) { var %ffrspname FER $+ %ffrbmtier }
n1210=            else { var -n %ffrspname FEAR }
n1211=          }
n1212=        }
n1213=        elseif ( $read(BlackSpell.txt,3) = Element: Death) {
n1214=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1215=            if (%ffrbmtier > 1) { var %ffrspname HNT $+ %ffrbmtier }
n1216=            else { var -n %ffrspname HANT }
n1217=          }
n1218=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1219=            if (%ffrbmtier > 1) { var %ffrspname PES $+ %ffrbmtier }
n1220=            else { var -n %ffrspname PESS }
n1221=          }
n1222=        }
n1223=        elseif ( $read(BlackSpell.txt,3) = Element: Fire) {
n1224=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1225=            if (%ffrbmtier > 1) { var %ffrspname STV $+ %ffrbmtier }
n1226=            else { var -n %ffrspname STAV }
n1227=          }
n1228=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1229=            if (%ffrbmtier > 1) { var %ffrspname IGN $+ %ffrbmtier }
n1230=            else { var -n %ffrspname IGNI }
n1231=          }
n1232=        }
n1233=        elseif ( $read(BlackSpell.txt,3) = Element: Earth) {
n1234=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1235=            if (%ffrbmtier > 1) { var %ffrspname TER $+ %ffrbmtier }
n1236=            else { var -n %ffrspname TERR }
n1237=          }
n1238=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1239=            if (%ffrbmtier > 1) { var %ffrspname GAP $+ %ffrbmtier }
n1240=            else { var -n %ffrspname GAPE }
n1241=          }
n1242=        }
n1243=        else {
n1244=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1245=            if (%ffrbmtier > 1) { var %ffrspname PSH $+ %ffrbmtier }
n1246=            else { var -n %ffrspname PUSH }
n1247=          }
n1248=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1249=            if (%ffrbmtier > 1) { var %ffrspname RPL $+ %ffrbmtier }
n1250=            else { var -n %ffrspname REPL }
n1251=          }
n1252=        }
n1253=      }
n1254=      elseif ( $read(BlackSpell.txt,2) = Effect: Locked) {
n1255=        var %ffrsptype fight
n1256=        var %ffrspgfxshape 14
n1257=        var %ffrspbatmsg 5 (Easy to hit)
n1258=        var %ffrspcommand set % $+ ffrspunmsg5 [Assigned]
n1259=        if ( $read(BlackSpell.txt,3) = Element: Time) {
n1260=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1261=            if (%ffrbmtier > 1) { var %ffrspname WEL $+ %ffrbmtier }
n1262=            else { var -n %ffrspname WELL }
n1263=          }
n1264=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1265=            if (%ffrbmtier > 1) { var %ffrspname GRV $+ %ffrbmtier }
n1266=            else { var -n %ffrspname GRAV }
n1267=          }
n1268=        }
n1269=        elseif ( $read(BlackSpell.txt,3) = Element: Ice) {
n1270=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1271=            if (%ffrbmtier > 1) { var %ffrspname FLO $+ %ffrbmtier }
n1272=            else { var -n %ffrspname FLOE }
n1273=          }
n1274=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1275=            if (%ffrbmtier > 1) { var %ffrspname GLC $+ %ffrbmtier }
n1276=            else { var -n %ffrspname GLAC }
n1277=          }
n1278=        }
n1279=        elseif ( $read(BlackSpell.txt,3) = Element: Lit) {
n1280=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1281=            if (%ffrbmtier > 1) { var %ffrspname BOL $+ %ffrbmtier }
n1282=            else { var -n %ffrspname BOLT }
n1283=          }
n1284=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1285=            if (%ffrbmtier > 1) { var %ffrspname WIR $+ %ffrbmtier }
n1286=            else { var -n %ffrspname WIRE }
n1287=          }
n1288=        }
n1289=        elseif ( $read(BlackSpell.txt,3) = Element: Earth) {
n1290=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1291=            if (%ffrbmtier > 1) { var %ffrspname PIT $+ %ffrbmtier }
n1292=            else { var -n %ffrspname PIT  $+ }
n1293=          }
n1294=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1295=            if (%ffrbmtier > 1) { var %ffrspname OUB $+ %ffrbmtier }
n1296=            else { var -n %ffrspname OUBL }
n1297=          }
n1298=        }
n1299=        else {
n1300=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) {
n1301=            if (%ffrbmtier > 1) { var %ffrspname LOK $+ %ffrbmtier }
n1302=            else { var -n %ffrspname LOCK }
n1303=          }
n1304=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) {
n1305=            if (%ffrbmtier > 1) { var %ffrspname SEL $+ %ffrbmtier }
n1306=            else { var -n %ffrspname SEAL }
n1307=          }
n1308=        }
n1309=      }
n1310=      elseif ( $read(BlackSpell.txt,2) = Effect: Dispel) {
n1311=        var %ffrspgfxshape 14
n1312=        var %ffrspbatmsg 29 (Defenseless)
n1313=        var %ffrspcommand set % $+ ffrspunmsg29 [Assigned]
n1314=        if ( $read(BlackSpell.txt,3) = Element: Status) {
n1315=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname NFIL }
n1316=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname NEUT }
n1317=        }
n1318=        elseif ( $read(BlackSpell.txt,3) = Element: Time) {
n1319=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname SET  $+ }
n1320=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname NULL }
n1321=        }
n1322=        elseif ( $read(BlackSpell.txt,3) = Element: Death) {
n1323=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname DSPL }
n1324=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname WIPE }
n1325=        }
n1326=        elseif ( $read(BlackSpell.txt,3) = Element: Lit) {
n1327=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname RIP  $+ }
n1328=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname NEG  $+ }
n1329=        }
n1330=        else {
n1331=          if ( $read(BlackSpell.txt,4) = Target: Single Enemy) { var -n %ffrspname VULN }
n1332=          elseif ( $read(BlackSpell.txt,4) = Target: Enemy Party) { var -n %ffrspname PURG }
n1333=        }
n1334=      }
n1335=      elseif ($read(BlackSpell.txt,w,*word*)) {
n1336=        var %ffrsptype word
n1337=        var %ffrspgfxshape 17
n1338=        if (Ice isincs $read(BlackSpell.txt,w,*element*)) { var %ffrspbatmsg 76 (Frozen) }
n1339=        if (time isin $read(BlackSpell.txt,w,*element*)) && (($read(BlackSpell.txt,w,*stun*)) || ($read(BlackSpell.txt,w,*coma*)) || ($read(BlackSpell.txt,w,*deb*))) {
n1340=          var %ffrspbatmsg 30 (Time stopped)
n1341=          var %ffrspcommand set % $+ ffrspunmsg30 [Assigned]
n1342=        }
n1343=        if ( $read(BlackSpell.txt,2) = Effect: Power Word "Debilitate" ) { var -n %ffrspname WEAK }
n1344=        elseif ( $read(BlackSpell.txt,2) = Effect: Power Word "Kill") {
n1345=          var -n %ffrspname XXXX
n1346=          if ($read(BlackSpell.txt,3) = Element: Poison) {
n1347=            var %ffrspbatmsg 77 (Poison smoke)
n1348=            var %ffrspcommand set % $+ ffrspunmsg77 [Assigned]
n1349=          }
n1350=          elseif ($read(BlackSpell.txt,3) = Element: Time) {
n1351=            var %ffrspbatmsg 31 (Exile to 4th dimension)
n1352=            var %ffrspcommand set % $+ ffrspunmsg31 [Assigned]
n1353=          }
n1354=          elseif ($read(BlackSpell.txt,3) = Element: Death) {
n1355=            var %ffrspbatmsg 21 (Erased)
n1356=            var %ffrspcommand set % $+ ffrspunmsg21 [Assigned]
n1357=          }
n1358=          elseif ($read(BlackSpell.txt,3) = Element: Earth) {
n1359=            var %ffrspbatmsg 22 (Fell into crack)
n1360=            var %ffrspcommand set % $+ ffrspunmsg22 [Assigned]
n1361=          }
n1362=        }
n1363=        elseif ( $read(BlackSpell.txt,2) = Effect: Power Word "Break") { var -n %ffrspname PETR }
n1364=        elseif ( $read(BlackSpell.txt,2) = Effect: Power Word "Decay" ) {
n1365=          if ($read(BlackSpell.txt,w,*fire*)) { unset %ffrspbatmsg }
n1366=          var -n %ffrspname DCAY
n1367=        }
n1368=        elseif ( $read(BlackSpell.txt,2) = Effect: Power Word "Blind" ) { var -n %ffrspname BLND }
n1369=        elseif ( $read(BlackSpell.txt,2) = Effect: Power Word "Stun" ) { var -n %ffrspname STUN }
n1370=        elseif ( $read(BlackSpell.txt,2) = Effect: Power Word "Coma" ) { var -n %ffrspname COMA }
n1371=      }
n1372=      else { echo 11 -s Debug: Unidentified Debuff ( $+ $read(BlackSpell.txt,2) $+ ) | write -al $+ %ffrspline SpellTable.txt BLACK DEBUFF }
n1373=    }
n1374=    else { echo 11 -s Debug: Unidentified Effect ( $+ $read(BlackSpell.txt,1) $+ ) | write -al $+ %ffrspline SpellTable.txt BLACK SPELL }
n1375=  }
n1376=  unset %ffrspredharm
n1377=
n1378=  ; Shape name translator
n1379=
n1380=  if (%ffrspgfxshape = 12) { var %ffrspgfxshape Bar of Light }
n1381=  elseif (%ffrspgfxshape = 13) { var %ffrspgfxshape Twinkling Bar }
n1382=  elseif (%ffrspgfxshape = 14) { var %ffrspgfxshape Twinkles }
n1383=  elseif (%ffrspgfxshape = 15) { var %ffrspgfxshape Merging Stars }
n1384=  elseif (%ffrspgfxshape = 16) { var %ffrspgfxshape Stars }
n1385=  elseif (%ffrspgfxshape = 17) { var %ffrspgfxshape Sparkling Bolt }
n1386=  elseif (%ffrspgfxshape = 18) { var %ffrspgfxshape Energy Bolt }
n1387=  elseif (%ffrspgfxshape = 19) { var %ffrspgfxshape Flaring Bolt }
n1388=  elseif (%ffrspgfxshape = 20) { var %ffrspgfxshape Energy Flare }
n1389=  elseif (%ffrspgfxshape = 21) { var %ffrspgfxshape Fizzling Flare }
n1390=  elseif (%ffrspgfxshape = 22) { var %ffrspgfxshape Glowing Ball }
n1391=  elseif (%ffrspgfxshape = 23) { var %ffrspgfxshape Bursting Ball }
n1392=  elseif (%ffrspgfxshape = 24) { var %ffrspgfxshape Magic Burst }
n1393=  elseif (%ffrspgfxshape = 25) { var %ffrspgfxshape Directed Burst }
n1394=  elseif (%ffrspgfxshape = 26) { var %ffrspgfxshape Palm Blast }
n1395=  elseif (%ffrspgfxshape = 27) { var %ffrspgfxshape Pointing Blast }
n1396=
n1397=  ; Starts writing to text file
n1398=  if (%ffrspname isincs $read(SpellTable.txt,w, $+ %ffrspname $+ *)) && ((%ffrspellbinding = 1) || ($read( $+ %ffrspmagic $+ Spell.txt,w,*resist*)) || ($read( $+ %ffrspmagic $+ Spell.txt,w,*double*)) || ($read( $+ %ffrspmagic $+ Spell.txt,w,*word*)) || (($read( $+ %ffrspmagic $+ Spell.txt,w,*heal*)) && ($read( $+ %ffrspmagic $+ Spell.txt,w,*effect*)))) {
n1399=    if (%ffrspmagic = black) && ($read(BlackSpell.txt,1) = Type: Damage) && ($read(BlackSpell.txt,w,*party*)) { dec %ffrspblackAoE 1 }
n1400=    if (%ffrspname = VOX) && (VOXa isincs $read(SpellTable.txt,w, $+ %ffrspname $+ *)) { /echo 12 -s VOX collided with VOXa
n1401=      if (!$read(SpellTable.txt,w, $+ %ffrspname $+ *, $+ $calc($readn +1))) { /echo 9 -s #FreeVOX | goto freevox }
n1402=      else { echo 4 -s VOX can only be freed once! }
n1403=    }
n1404=    echo 4 -s %ffrspname already exists! Generating new spell.
n1405=    unset %ffrspcommand
n1406=    inc %ffrspreroll 1
n1407=    goto spdupe
n1408=  }
n1409=  else {
n1410=    :freevox
n1411=    if (%ffrsptype = blank) { write -a SpellTable.txt %ffrspname }
n1412=    if (%ffrsptype = hinder) { write -a SpellTable.txt %ffrspname $+ : $read( $+ %ffrspmagic $+ Spell.txt,w,*bonus*) }
n1413=    if (%ffrsptype = help) { write -a SpellTable.txt %ffrspname $+ : $read( $+ %ffrspmagic $+ Spell.txt,w,*power*) }
n1414=    if (%ffrsptype = fight) { write -a SpellTable.txt %ffrspname $+ : $read( $+ %ffrspmagic $+ Spell.txt,w,*power*) $+ , $read( $+ %ffrspmagic $+ Spell.txt,w,*bonus*) }
n1415=    if (%ffrsptype = toxin) { write -a SpellTable.txt %ffrspname $+ : $read( $+ %ffrspmagic $+ Spell.txt,w,*bonus*) ( $+ $read( $+ %ffrspmagic $+ Spell.txt,w,*element*) $+ ) }
n1416=    if (%ffrsptype = word) { write -a SpellTable.txt %ffrspname $+ : Power Word ( $+ $read( $+ %ffrspmagic $+ Spell.txt,w,*element*) $+ ) }
n1417=    write -al $+ %ffrspline SpellTable.txt - Shape: %ffrspgfxshape $+ , Colour: %ffrspgfxclr $+ , Message: %ffrspbatmsg
n1418=  }
n1419=  unset %ffrspbatmsg
n1420=  %ffrspcommand
n1421=  unset %ffrspcommand
n1422=  if (($read( $+ %ffrspmagic $+ Spell.txt,w,*poison*)) || ($read( $+ %ffrspmagic $+ Spell.txt,w,*dark*)) || ($read( $+ %ffrspmagic $+ Spell.txt,w,*conf*))) && (fire isin $read( $+ %ffrspmagic $+ Spell.txt,w,*element*)) { var %ffrsp76ablaze 1 }
n1423=  elseif (($read( $+ %ffrspmagic $+ Spell.txt,w,*debuff*)) || ($read( $+ %ffrspmagic $+ Spell.txt,w,*kill*))) && (Ice isincs $read( $+ %ffrspmagic $+ Spell.txt,w,*element*)) && (!$read( $+ %ffrspmagic $+ Spell.txt,w,*slow*)) && (!$read( $+ %ffrspmagic $+ Spell.txt,w,*lock*)) { var %ffrsp76frozen 1 }
n1424=  elseif ($read(SpellTable.txt,w,*BSRK*)) { var %ffrsp76go.mad 1 }
n1425=  echo 0 -s Debug: Spellbound %ffrspmagic %ffrsplevel $+ - $+ %ffrspslot ( $+ $read( $+ %ffrspmagic $+ Spell.txt,1) $+ , Tier %ffrwmtier $+ %ffrbmtier )
n1426=  if (i isincs $1) {
n1427=    if (%ffrspmagic = white) {
n1428=      if (%ffrsplightaxe != 1) && ($read(WhiteSpell.txt,3) = Target: Enemy Party) && (!$read(WhiteSpell.txt,w,*word*)) {
n1429=        if (%ffrsplevel > 2) {
n1430=          ; write -al $+ %ffrspline SpellTable.txt [Light Axe]
n1431=          set %ffrsplightaxe 1
n1432=        }
n1433=        set %ffrspharm %ffrspline
n1434=      }
n1435=      if (%ffrsphealgear != 1) && ($read(WhiteSpell.txt,1) = Type: Heal) && (($read(WhiteSpell.txt,2) = Target: All Allies) || ($read(WhiteSpell.txt,3) = Target: All Allies)) {
n1436=        if (%ffrsplevel > 2) {
n1437=          ; write -al $+ %ffrspline SpellTable.txt [Heal Staff/Helm]
n1438=          set %ffrsphealgear 1
n1439=        }
n1440=        set %ffrspgrouphug %ffrspline
n1441=      }
n1442=      if (%ffrspdefense != 1) && ($read(WhiteSpell.txt,1) = Type: Buff) && (($read(WhiteSpell.txt,3) = Target: Caster) || ($read(WhiteSpell.txt,3) = Target: Single Ally)) && (($read(WhiteSpell.txt,2) = Effect: Absorb Up) || ($read(WhiteSpell.txt,2) = Effect: Evade Up) || ($read(WhiteSpell.txt,w,*resist*))) {
n1443=        if (%ffrsplevel > 2) {
n1444=          ; write -al $+ %ffrspline SpellTable.txt [Defense]
n1445=          set %ffrspdefense 1
n1446=        }
n1447=        set %ffrspdefender %ffrspline
n1448=      }
n1449=      if (%ffrspwhiteshirt != 1) && ($read(WhiteSpell.txt,1) = Type: Buff) && ($read(WhiteSpell.txt,3) = Target: All Allies) && (($read(WhiteSpell.txt,w,*absorb*)) || ($read(WhiteSpell.txt,w,*evade*)) || ($read(WhiteSpell.txt,w,*resist*))) {
n1450=        if (%ffrsplevel > 2) {
n1451=          ; write -al $+ %ffrspline SpellTable.txt [White Shirt]
n1452=          set %ffrspwhiteshirt 1
n1453=        }
n1454=        set %ffrspcoverall %ffrspline
n1455=      }
n1456=      if (%ffrsppowerbonk != 1) && ($read(WhiteSpell.txt,1) = Type: Buff) && (($read(WhiteSpell.txt,2) = Effect: Double Hits) || ($read(WhiteSpell.txt,2) = Effect: Attack Up)) {
n1457=        if (%ffrsplevel > 4) {
n1458=          ; write -al $+ %ffrspline SpellTable.txt [Power Glove]
n1459=          set %ffrsppowerbonk 1
n1460=        }
n1461=        set %ffrspbonk %ffrspline
n1462=      }
n1463=    }
n1464=    elseif (%ffrspmagic = black) {
n1465=      if (%ffrspthorzeus != 1) && (($read(BlackSpell.txt,2) = Element: Lit) || ($read(BlackSpell.txt,3) = Element: Lit)) && ($read(BlackSpell.txt,w,*party*)) && (!$read(BlackSpell.txt,w,*word*)) {
n1466=        if (%ffrsplevel > 2) {
n1467=          ; write -al $+ %ffrspline SpellTable.txt [Thor/Zeus]
n1468=          set %ffrspthorzeus 1
n1469=        }
n1470=        set %ffrsplightning %ffrspline
n1471=      }
n1472=      if (%ffrspbanesword != 1) && (($read(BlackSpell.txt,w,*poison*)) || ($read(BlackSpell.txt,w,*smoke*))) && ($read(BlackSpell.txt,w,*party*)) && (!$read(BlackSpell.txt,w,*word*)) {
n1473=        if (%ffrsplevel > 4) {
n1474=          ; write -al $+ %ffrspline SpellTable.txt [Bane Sword]
n1475=          set %ffrspbanesword 1
n1476=        }
n1477=        set %ffrsptoxin %ffrspline
n1478=      }
n1479=      if (%ffrspmagestaff != 1) && ($read(BlackSpell.txt,1) = Type: Damage) && ($read(BlackSpell.txt,3) = Target: Enemy Party) && (%ffrsplightning != %ffrspline) && (%ffrsptoxin != %ffrspline) {
n1480=        if (%ffrsplevel > 2) {
n1481=          ; write -al $+ %ffrspline SpellTable.txt [Mage Staff]
n1482=          set %ffrspmagestaff 1
n1483=        }
n1484=        set %ffrspweakbomb %ffrspline
n1485=      }
n1486=      if (%ffrspdefense != 1) && ($read(BlackSpell.txt,1) = Type: Buff) && ($read(BlackSpell.txt,w,*allies*) != Target: All Allies) && (($read(BlackSpell.txt,2) = Effect: Absorb Up) || ($read(BlackSpell.txt,2) = Effect: Evade Up) || ($read(BlackSpell.txt,w,*resist*))) {
n1487=        if (%ffrsplevel > 4) {
n1488=          ; write -al $+ %ffrspline SpellTable.txt [Defense]
n1489=          set %ffrspdefense 1
n1490=        }
n1491=        set %ffrspdefender %ffrspline
n1492=      }
n1493=      if (%ffrspwizardstaff != 1) && ($read(BlackSpell.txt,1) = Type: Debuff) && ($read(BlackSpell.txt,4) = Target: Enemy Party) && (!$read(BlackSpell.txt,w,*word*)) && (%ffrsplightning != %ffrspline) && (%ffrsptoxin != %ffrspline) {
n1494=        if (%ffrsplevel > 4) {
n1495=          ; write -al $+ %ffrspline SpellTable.txt [Wizard Staff]
n1496=          set %ffrspwizardstaff 1
n1497=        }
n1498=        set %ffrspdebuffer %ffrspline
n1499=      }
n1500=      if (%ffrspblackshirt != 1) && ($read(BlackSpell.txt,1) = Type: Damage) && ($read(BlackSpell.txt,3) = Target: Enemy Party) && (%ffrspweakbomb != %ffrspline) && (%ffrsplightning != %ffrspline) && (%ffrsptoxin != %ffrspline) {
n1501=        if (%ffrsplevel > 3) {
n1502=          ; write -al $+ %ffrspline SpellTable.txt [Black Shirt]
n1503=          set %ffrspblackshirt 1
n1504=        }
n1505=        set %ffrspbigbomb %ffrspline
n1506=      }
n1507=      if (%ffrsppowerbonk != 1) && ($read(BlackSpell.txt,1) = Type: Buff) && (($read(BlackSpell.txt,2) = Effect: Double Hits) || ($read(BlackSpell.txt,2) = Effect: Attack Up)) {
n1508=        if (%ffrsplevel > 2) {
n1509=          ; write -al $+ %ffrspline SpellTable.txt [Power Glove]
n1510=          set %ffrsppowerbonk 1
n1511=        }
n1512=        set %ffrspbonk %ffrspline
n1513=      }
n1514=    }
n1515=  }
n1516=  :damagecheck
n1517=  if (b isincs $1) && (%ffrspspellbomb != 1) && (%ffrspblackAoE = 6) {
n1518=    echo 9 -s Minimum Spellbomb Quota Met!
n1519=    var %ffrspspellbomb 1
n1520=  }
n1521=  if (%ffrspallspells = 1) { echo 12 -s Debug: Trying to jump to output | goto sanitysuccess }
n1522=  unset %ffrwmtier
n1523=  unset %ffrbmtier
n1524=  if (%ffrspslot = 4) {
n1525=    if (%ffrspmagic = white) {
n1526=      write -a SpellTable.txt -
n1527=      inc %ffrspline 1
n1528=      set %ffrspslot 0
n1529=      set %ffrspmagic black
n1530=      goto sploop
n1531=    }
n1532=    elseif (%ffrspmagic = black) {
n1533=      write -a SpellTable.txt =
n1534=      inc %ffrspline 1
n1535=      if (%ffrsplevel = 8) {
n1536=        var %ffrspallspells 1
n1537=        var %ffrspbatmsgloop 0
n1538=        if (!%ffrspresmsg1) { set %ffrspresmsg1 Defend lightning [Unassigned] | dec %ffrspreslength 9 }
n1539=        if (!%ffrspresmsg2) { set %ffrspresmsg2 Defend fire [Unassigned] | dec %ffrspreslength 4 }
n1540=        if (!%ffrspresmsg3) { set %ffrspresmsg3 Defend cold [Unassigned] | dec %ffrspreslength 4 }
n1541=        if (!%ffrspresmsg4) { set %ffrspresmsg4 Defend magic [Unassigned] | dec %ffrspreslength 5 }
n1542=        if (!%ffrspresmsg5) { set %ffrspresmsg5 Defend all [Unassigned] }
n1543=        if (%ffrsp76frozen = 1) { set %ffrspmsg76 Frozen }
n1544=        elseif (%ffrsp76go.mad = 1) { set %ffrspmsg76 Go mad }
n1545=        elseif (%ffrsp76ablaze = 1) { set %ffrspmsg76 Ablaze }
n1546=        if ($read(SpellTable.txt,w,*BSRK*)) {
n1547=          if (%ffrspmsg76 = Frozen) {
n1548=            write -al $+ $readn SpellTable.txt 18 (Quick Shot)
n1549=            set %ffrspunmsg18 [Assigned]
n1550=          }
n1551=          else { write -al $+ $readn SpellTable.txt 76 (Go mad) }
n1552=        }
n1553=        if (%ffrspmsg76 != Ablaze) {
n1554=          if ($read(SpellTable.txt,w,*Element: Fire*)) || ($read(SpellTable.txt,w,*PYRO*)) || ($read(SpellTable.txt,w,*TORC*)) || (($read(SpellTable.txt,w,*Element: Fire*)) && (DCAY isin $read(SpellTable.txt, $readn ))) && (None !isin $read(SpellTable.txt, $readn )) && (Smoke !isin $read(SpellTable.txt, $readn )) {
n1555=            if (element isin $read(SpellTable.txt, $readn )) { write -al $+ $readn SpellTable.txt 77 (Poison smoke) | set %ffrspunmsg77 [Assigned] }
n1556=            else { write -al $+ $readn SpellTable.txt 0; None }
n1557=            while ($read(SpellTable.txt,w,*Element: Fire*, $calc( $readn +1))) || ($read(SpellTable.txt,w,*PYRO*)) || ($read(SpellTable.txt,w,*TORC*)) || (($read(SpellTable.txt,w,*Element: Fire*, $calc( $readn +1))) && (DCAY isin $read(SpellTable.txt, $readn ))) && (None !isin $read(SpellTable.txt, $readn )) && (Smoke !isin $read(SpellTable.txt, $readn )) {
n1558=              if (element isin $read(SpellTable.txt, $readn )) && (DCAY !isin $read(SpellTable.txt, $readn )) { write -al $+ $readn SpellTable.txt 77 (Poison smoke) | set %ffrspunmsg77 [Assigned] }
n1559=              else { write -al $+ $readn SpellTable.txt 0; None }
n1560=            }
n1561=            ; lol the while loops aren't working
n1562=            ; they're just finding the first one again and breaking the while loop
n1563=            ; even power words and debilitators are breaking it :)))
n1564=            ; it's 2:30 AM i'm not dealing with this
n1565=            ; EDIT FROM 2 YEARS IN THE FUTURE: oh my god did I just not check for confusion further up?????
n1566=          }
n1567=        }
n1568=        else {
n1569=          if ($read(SpellTable.txt,w,*Element: Fire*)) || ($read(SpellTable.txt,w,*PYRO*)) || ($read(SpellTable.txt,w,*TORC*)) || (($read(SpellTable.txt,w,*Element: Fire*)) && (DCAY isin $read(SpellTable.txt, $readn ))) && (None !isin $read(SpellTable.txt, $readn )) && (Smoke !isin $read(SpellTable.txt, $readn )) {
n1570=            write -al $+ $readn SpellTable.txt 76 (Ablaze)
n1571=            while ($read(SpellTable.txt,w,*Element: Fire*, $calc( $readn +1))) || ($read(SpellTable.txt,w,*PYRO*, $calc( $readn +1))) || ($read(SpellTable.txt,w,*TORC*, $calc( $readn +1))) || (($read(SpellTable.txt,w,*Element: Fire*, $calc( $readn +1))) && (DCAY isin $read(SpellTable.txt, $readn ))) && (blaz !isin $read(SpellTable.txt, $readn )) {
n1572=              write -al $+ $readn SpellTable.txt 76 (Ablaze)
n1573=            }
n1574=          }
n1575=        }
n1576=        :sanitysuccess
n1577=        if (%ffrspblackAoE < 6) && (b isincs %ffrspflags) {
n1578=          echo 8 -s Not Enough Spellbombs!
n1579=          write -a SpellTable.txt DEBUG FILE -- DO NOT USE
n1580=          batmsg
n1581=          run SpellTable.txt
n1582=          /timer 1 1 unset %ffr*
n1583=          /timer 1 5 /spell $1
n1584=          echo 14 -s Taking a break to breathe...
n1585=          halt
n1586=        }
n1587=        itemcheck
n1588=        batmsg
n1589=        /run SpellTable.txt
n1590=        if (%ffrspitems = 1) || (i !isincs %ffrspflags) { echo 9 -s Generated 64 Spells! Used flags: %ffrspflags }
n1591=        /timer 1 1 unset %ffr*
n1592=        /timer 1 1 halt
n1593=      }
n1594=      else {
n1595=        inc %ffrsplevel 1
n1596=        set %ffrspslot 0
n1597=        set %ffrspmagic white
n1598=        goto sploop
n1599=      }
n1600=    }
n1601=  }
n1602=  else { goto sploop }
n1603=}
