�
eC:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Proxy\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str  
)  !
]! "
[		 
assembly		 	
:			 

AssemblyDescription		 
(		 
$str		 !
)		! "
]		" #
[

 
assembly

 	
:

	 
!
AssemblyConfiguration

  
(

  !
$str

! #
)

# $
]

$ %
[ 
assembly 	
:	 

AssemblyCompany 
( 
$str 
) 
] 
[ 
assembly 	
:	 

AssemblyProduct 
( 
$str "
)" #
]# $
[ 
assembly 	
:	 

AssemblyCopyright 
( 
$str 0
)0 1
]1 2
[ 
assembly 	
:	 

AssemblyTrademark 
( 
$str 
)  
]  !
[ 
assembly 	
:	 

AssemblyCulture 
( 
$str 
) 
] 
[ 
assembly 	
:	 


ComVisible 
( 
false 
) 
] 
[ 
assembly 	
:	 

Guid 
( 
$str 6
)6 7
]7 8
[   
assembly   	
:  	 

AssemblyVersion   
(   
$str   $
)  $ %
]  % &
[!! 
assembly!! 	
:!!	 

AssemblyFileVersion!! 
(!! 
$str!! (
)!!( )
]!!) *�]
WC:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Proxy\ProxyUser.cs
	namespace 	
Proxy
 
{ 
public 

class 
	ProxyUser 
: 
IUserService )
{ 
string 
BaseAddress 
= 
$str 6
;6 7
private 
async 
Task 
< 
T 
> 
SendPost &
<& '
T' (
,( )
PostData* 2
>2 3
(3 4
string4 :

requestURI; E
,E F
PostDataG O
dataP T
)T U
{ 	
T 
Result 
= 
default 
( 
T  
)  !
;! "
using 
( 
var 
Client 
= 
new  #

HttpClient$ .
(. /
)/ 0
)0 1
{ 
try 
{ 

requestURI 
=  
BaseAddress! ,
+- .

requestURI/ 9
;9 :
Client 
. !
DefaultRequestHeaders 0
.0 1
Accept1 7
.7 8
Clear8 =
(= >
)> ?
;? @
Client 
. !
DefaultRequestHeaders 0
.0 1
Accept1 7
.7 8
Add8 ;
(; <
new< ?+
MediaTypeWithQualityHeaderValue@ _
(_ `
$str` r
)r s
)s t
;t u
var 
JSONData  
=! "
JsonConvert# .
.. /
SerializeObject/ >
(> ?
data? C
)C D
;D E
var 
Response  
=! "
await# (
Client) /
./ 0
	PostAsync0 9
(9 :

requestURI: D
,D E
newF I
StringContentJ W
(W X
JSONDataX `
,` a
Encodingb j
.j k
UTF8k o
,o p
$str	q �
)
� �
)
� �
;
� �
var!! 
ResultWebAPI!! $
=!!% &
await!!' ,
Response!!- 5
.!!5 6
Content!!6 =
.!!= >
ReadAsStringAsync!!> O
(!!O P
)!!P Q
;!!Q R
Result"" 
="" 
JsonConvert"" (
.""( )
DeserializeObject"") :
<"": ;
T""; <
>""< =
(""= >
ResultWebAPI""> J
)""J K
;""K L
}## 
catch$$ 
($$ 
	Exception$$  
ex$$! #
)$$# $
{%% 
Console&& 
.&& 
	WriteLine&& %
(&&% &
$"&&& (
$str&&( ;
{&&; <
ex&&< >
.&&> ?
Message&&? F
}&&F G
"&&G H
)&&H I
;&&I J
}'' 
}(( 
return)) 
Result)) 
;)) 
}** 	
private-- 
async-- 
Task-- 
<-- 
T-- 
>-- 
SendGet-- %
<--% &
T--& '
>--' (
(--( )
string--) /

requestURI--0 :
)--: ;
{.. 	
T// 
Result// 
=// 
default// 
(// 
T//  
)//  !
;//! "
using00 
(00 
var00 
Client00 
=00 
new00  #

HttpClient00$ .
(00. /
)00/ 0
)000 1
{11 
try22 
{33 

requestURI44 
=44  
BaseAddress44! ,
+44- .

requestURI44/ 9
;449 :
Client55 
.55 !
DefaultRequestHeaders55 0
.550 1
Accept551 7
.557 8
Clear558 =
(55= >
)55> ?
;55? @
Client66 
.66 !
DefaultRequestHeaders66 0
.660 1
Accept661 7
.667 8
Add668 ;
(66; <
new66< ?+
MediaTypeWithQualityHeaderValue66@ _
(66_ `
$str66` r
)66r s
)66s t
;66t u
var88 

ResultJSON88 "
=88# $
await88% *
Client88+ 1
.881 2
GetStringAsync882 @
(88@ A

requestURI88A K
)88K L
;88L M
Result99 
=99 
JsonConvert99 (
.99( )
DeserializeObject99) :
<99: ;
T99; <
>99< =
(99= >

ResultJSON99> H
)99H I
;99I J
}:: 
catch;; 
(;; 
	Exception;;  
ex;;! #
);;# $
{<< 
Console== 
.== 
	WriteLine== %
(==% &
$"==& (
$str==( :
{==: ;
ex==; =
.=== >
Message==> E
}==E F
"==F G
)==G H
;==H I
}>> 
}?? 
return@@ 
Result@@ 
;@@ 
}AA 	
publicDD  
UserCreationResponseDD #
CreateDD$ *
(DD* +
UserDD+ /
usersDD0 5
)DD5 6
{EE 	
tryFF 
{GG 
varHH 
responseHH 
=HH 
TaskHH #
.HH# $
RunHH$ '
(HH' (
asyncHH( -
(HH. /
)HH/ 0
=>HH1 3
awaitHH4 9
SendPostHH: B
<HHB C 
UserCreationResponseHHC W
,HHW X
UserHHY ]
>HH] ^
(HH^ _
$strHH_ u
,HHu v
usersHHw |
)HH| }
)HH} ~
.HH~ 
Result	HH �
;
HH� �
ifJJ 
(JJ 
responseJJ 
!=JJ 
nullJJ  $
)JJ$ %
{KK 
returnLL 
responseLL #
;LL# $
}MM 
elseNN 
{OO 
throwPP 
newPP 
	ExceptionPP '
(PP' (
$strPP( F
)PPF G
;PPG H
}QQ 
}RR 
catchSS 
(SS 
	ExceptionSS 
exSS 
)SS  
{TT 
throwUU 
newUU 
	ExceptionUU #
(UU# $
$"UU$ &
$strUU& >
{UU> ?
exUU? A
.UUA B
MessageUUB I
}UUI J
"UUJ K
)UUK L
;UUL M
}VV 
}WW 	
publicZZ 
asyncZZ 
TaskZZ 
<ZZ  
UserCreationResponseZZ .
>ZZ. /
VerifyAndCreateUserZZ0 C
(ZZC D
stringZZD J
emailZZK P
,ZZP Q
stringZZR X
codeZZY ]
)ZZ] ^
{[[ 	
var\\ 
verifyRequest\\ 
=\\ 
new\\  #
VerifyCodeRequest\\$ 5
{]] 
Email^^ 
=^^ 
email^^ 
,^^ 
Code__ 
=__ 
code__ 
}`` 
;`` 
trybb 
{cc 
varee 
responseee 
=ee 
awaitee $
SendPostee% -
<ee- . 
UserCreationResponseee. B
,eeB C
VerifyCodeRequesteeD U
>eeU V
(eeV W
$streeW v
,eev w
verifyRequest	eex �
)
ee� �
;
ee� �
ifgg 
(gg 
responsegg 
!=gg 
nullgg  $
)gg$ %
{hh 
returnii 
responseii #
;ii# $
}jj 
elsekk 
{ll 
throwmm 
newmm 
	Exceptionmm '
(mm' (
$strmm( \
)mm\ ]
;mm] ^
}nn 
}oo 
catchpp 
(pp 
	Exceptionpp 
expp 
)pp  
{qq 
throwrr 
newrr 
	Exceptionrr #
(rr# $
$"rr$ &
$strrr& W
{rrW X
exrrX Z
.rrZ [
Messagerr[ b
}rrb c
"rrc d
)rrd e
;rre f
}ss 
}tt 	
publicww 
asyncww 
Taskww 
<ww 
stringww  
>ww  !
Loginww" '
(ww' (
stringww( .
emailww/ 4
,ww4 5
stringww6 <
passwordww= E
)wwE F
{xx 	
varyy 
loginRequestyy 
=yy 
newyy "
LoginRequestyy# /
{zz 
Email{{ 
={{ 
email{{ 
,{{ 
Password|| 
=|| 
password|| #
}}} 
;}} 
try 
{
�� 
var
�� 
response
�� 
=
�� 
await
�� $
SendPost
��% -
<
��- .
string
��. 4
,
��4 5
LoginRequest
��6 B
>
��B C
(
��C D
$str
��D U
,
��U V
loginRequest
��W c
)
��c d
;
��d e
if
�� 
(
�� 
!
�� 
string
�� 
.
�� 
IsNullOrEmpty
�� )
(
��) *
response
��* 2
)
��2 3
)
��3 4
{
�� 
return
�� 
response
�� #
;
��# $
}
�� 
else
�� 
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( G
)
��G H
;
��H I
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
$"
��$ &
$str
��& ?
{
��? @
ex
��@ B
.
��B C
Message
��C J
}
��J K
"
��K L
)
��L M
;
��M N
}
�� 
}
�� 	
public
�� 
async
�� 
Task
�� 
<
�� 
LoginResponse
�� '
>
��' (

VerifyCode
��) 3
(
��3 4
string
��4 :
email
��; @
,
��@ A
string
��B H
code
��I M
)
��M N
{
�� 	
var
�� 
verifyRequest
�� 
=
�� 
new
��  #
VerifyCodeRequest
��$ 5
{
�� 
Email
�� 
=
�� 
email
�� 
,
�� 
Code
�� 
=
�� 
code
�� 
}
�� 
;
�� 
try
�� 
{
�� 
var
�� 
response
�� 
=
�� 
await
�� $
SendPost
��% -
<
��- .
LoginResponse
��. ;
,
��; <
VerifyCodeRequest
��= N
>
��N O
(
��O P
$str
��P f
,
��f g
verifyRequest
��h u
)
��u v
;
��v w
if
�� 
(
�� 
response
�� 
!=
�� 
null
��  $
)
��$ %
{
�� 
return
�� 
response
�� #
;
��# $
}
�� 
else
�� 
{
�� 
throw
�� 
new
�� 
	Exception
�� '
(
��' (
$str
��( X
)
��X Y
;
��Y Z
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
throw
�� 
new
�� 
	Exception
�� #
(
��# $
$"
��$ &
$str
��& D
{
��D E
ex
��E G
.
��G H
Message
��H O
}
��O P
"
��P Q
)
��Q R
;
��R S
}
�� 
}
�� 	
}
�� 
}�� 