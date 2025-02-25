�	
sD:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\Global.asax.cs
	namespace		 	
Presentation		
 
{

 
public 

class 
MvcApplication 
:  !
System" (
.( )
Web) ,
., -
HttpApplication- <
{ 
	protected 
void 
Application_Start (
(( )
)) *
{ 	
AreaRegistration 
. 
RegisterAllAreas -
(- .
). /
;/ 0
FilterConfig 
. !
RegisterGlobalFilters .
(. /
GlobalFilters/ <
.< =
Filters= D
)D E
;E F
RouteConfig 
. 
RegisterRoutes &
(& '

RouteTable' 1
.1 2
Routes2 8
)8 9
;9 :
BundleConfig 
. 
RegisterBundles (
(( )
BundleTable) 4
.4 5
Bundles5 <
)< =
;= >
} 	
} 
} �>
�D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\Controllers\UserDataController.cs
	namespace 	
Presentation
 
. 
Controllers "
{ 
public		 

class		 
UserDataController		 #
:		$ %

Controller		& 0
{

 
[ 	
HttpGet	 
] 
public 
async 
Task 
< 
ActionResult &
>& '
ListUserData( 4
(4 5
)5 6
{ 	
var 
proxyService 
= 
new "
	ProxyUser# ,
(, -
)- .
;. /
try 
{ 
var 
response 
= 
await $
proxyService% 1
.1 2
GetAllUsers2 =
(= >
)> ?
;? @
if 
( 
response 
. 
Success $
)$ %
{ 
return 
View 
(  
response  (
.( )
Users) .
). /
;/ 0
} 
else 
{ 
TempData 
[ 
$str +
]+ ,
=- .
response/ 7
.7 8
Message8 ?
;? @
return 
View 
(  
)  !
;! "
} 
} 
catch 
( 
	Exception 
ex 
)  
{ 
TempData 
[ 
$str '
]' (
=) *
ex+ -
.- .
Message. 5
;5 6
return   
View   
(   
)   
;   
}!! 
}"" 	
[%% 	
HttpGet%%	 
]%% 
public&& 
async&& 
Task&& 
<&& 
ActionResult&& &
>&&& '
GetUserDataById&&( 7
(&&7 8
int&&8 ;
userId&&< B
)&&B C
{'' 	
var(( 
proxyService(( 
=(( 
new(( "
ProxyUserData((# 0
(((0 1
)((1 2
;((2 3
try)) 
{** 
var++ 
response++ 
=++ 
await++ $
proxyService++% 1
.++1 2
GetUserDataById++2 A
(++A B
userId++B H
)++H I
;++I J
if-- 
(-- 
response-- 
==-- 
null--  $
)--$ %
{.. 
TempData// 
[// 
$str// +
]//+ ,
=//- .
$str/// U
;//U V
return00 
RedirectToAction00 +
(00+ ,
$str00, :
)00: ;
;00; <
}11 
if33 
(33 
response33 
.33 
Success33 $
)33$ %
{44 
return55 
View55 
(55  
$str55  8
,558 9
response55: B
.55B C
	User_Data55C L
)55L M
;55M N
}66 
else77 
{88 
TempData99 
[99 
$str99 +
]99+ ,
=99- .
response99/ 7
.997 8
Message998 ?
;99? @
return:: 
RedirectToAction:: +
(::+ ,
$str::, :
)::: ;
;::; <
};; 
}<< 
catch== 
(== 
	Exception== 
ex== 
)==  
{>> 
TempData?? 
[?? 
$str?? '
]??' (
=??) *
ex??+ -
.??- .
Message??. 5
;??5 6
return@@ 
RedirectToAction@@ '
(@@' (
$str@@( 6
)@@6 7
;@@7 8
}AA 
}BB 	
[FF 	
HttpGetFF	 
]FF 
publicGG 
asyncGG 
TaskGG 
<GG 
ActionResultGG &
>GG& '"
CreateOrUpdateUserDataGG( >
(GG> ?
intGG? B
userIdGGC I
)GGI J
{HH 	
varII 
proxyServiceII 
=II 
newII "
ProxyUserDataII# 0
(II0 1
)II1 2
;II2 3
tryKK 
{LL 
varMM 
responseMM 
=MM 
awaitMM $
proxyServiceMM% 1
.MM1 2
GetUserDataByIdMM2 A
(MMA B
userIdMMB H
)MMH I
;MMI J
ifOO 
(OO 
responseOO 
!=OO 
nullOO  $
&&OO% '
responseOO( 0
.OO0 1
SuccessOO1 8
)OO8 9
{PP 
returnRR 
ViewRR 
(RR  
responseRR  (
.RR( )
	User_DataRR) 2
)RR2 3
;RR3 4
}SS 
elseTT 
{UU 
returnWW 
ViewWW 
(WW  
newWW  #
	User_DataWW$ -
{WW. /
user_idWW0 7
=WW8 9
userIdWW: @
}WWA B
)WWB C
;WWC D
}XX 
}YY 
catchZZ 
(ZZ 
	ExceptionZZ 
exZZ 
)ZZ  
{[[ 
TempData\\ 
[\\ 
$str\\ '
]\\' (
=\\) *
ex\\+ -
.\\- .
Message\\. 5
;\\5 6
return]] 
RedirectToAction]] '
(]]' (
$str]]( 6
)]]6 7
;]]7 8
}^^ 
}__ 	
[cc 	
HttpPostcc	 
]cc 
publicdd 
asyncdd 
Taskdd 
<dd 
ActionResultdd &
>dd& '"
CreateOrUpdateUserDatadd( >
(dd> ?
	User_Datadd? H
userDataddI Q
)ddQ R
{ee 	
varff 
proxyServiceff 
=ff 
newff "
ProxyUserDataff# 0
(ff0 1
)ff1 2
;ff2 3
trygg 
{hh 
varjj 
responsejj 
=jj 
userDatajj '
.jj' (
user_data_idjj( 4
==jj5 7
$numjj8 9
?kk 
awaitkk 
proxyServicekk (
.kk( )
CreateUserDatakk) 7
(kk7 8
userDatakk8 @
)kk@ A
:ll 
awaitll 
proxyServicell (
.ll( )
UpdateUserDatall) 7
(ll7 8
userDatall8 @
.ll@ A
user_data_idllA M
,llM N
userDatallO W
)llW X
;llX Y
ifnn 
(nn 
responsenn 
.nn 
Successnn $
)nn$ %
{oo 
TempDatapp 
[pp 
$strpp -
]pp- .
=pp/ 0
$strpp1 \
;pp\ ]
returnqq 
RedirectToActionqq +
(qq+ ,
$strqq, =
,qq= >
newqq? B
{qqC D
userIdqqE K
=qqL M
userDataqqN V
.qqV W
user_idqqW ^
}qq_ `
)qq` a
;qqa b
}rr 
elsess 
{tt 
TempDatauu 
[uu 
$struu +
]uu+ ,
=uu- .
responseuu/ 7
.uu7 8
Messageuu8 ?
;uu? @
returnvv 
Viewvv 
(vv  
userDatavv  (
)vv( )
;vv) *
}ww 
}xx 
catchyy 
(yy 
	Exceptionyy 
exyy 
)yy  
{zz 
TempData{{ 
[{{ 
$str{{ '
]{{' (
={{) *
$"{{+ -
$str{{- 4
{{{4 5
ex{{5 7
.{{7 8
Message{{8 ?
}{{? @
"{{@ A
;{{A B
return|| 
View|| 
(|| 
userData|| $
)||$ %
;||% &
}}} 
}~~ 	
} 
}�� �
D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str '
)' (
]( )
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
$str )
)) *
]* +
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
["" 
assembly"" 	
:""	 

AssemblyVersion"" 
("" 
$str"" $
)""$ %
]""% &
[## 
assembly## 	
:##	 

AssemblyFileVersion## 
(## 
$str## (
)##( )
]##) *��
�D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\Controllers\UserController.cs
	namespace 	
Presentation
 
. 
Controllers "
{ 
public 

class 
UserController 
:  !

Controller" ,
{ 
private 
readonly 
IEmailService &
_emailService' 4
;4 5
public 
UserController 
( 
) 
{ 	
_emailService 
= 
new 
EmailService  ,
(, -
)- .
;. /
} 	
public 
ActionResult 
Index !
(! "
)" #
{ 	
return 
View 
( 
) 
; 
} 	
public 
ActionResult 

CreateUser &
(& '
)' (
{ 	
return 
View 
( 
) 
; 
}   	
["" 	
HttpPost""	 
]"" 
[## 	$
ValidateAntiForgeryToken##	 !
]##! "
public$$ 
ActionResult$$ 

CreateUser$$ &
($$& '
User$$' +
newUser$$, 3
)$$3 4
{%% 	
var&& 
proxy_service&& 
=&& 
new&&  #
	ProxyUser&&$ -
(&&- .
)&&. /
;&&/ 0
if(( 
((( 

ModelState(( 
.(( 
IsValid(( "
)((" #
{)) 
try** 
{++ 
var,, 
createdUserResponse,, +
=,,, -
proxy_service,,. ;
.,,; <
Create,,< B
(,,B C
newUser,,C J
),,J K
;,,K L
if.. 
(.. 
createdUserResponse.. +
...+ ,
Success.., 3
)..3 4
{// 
TempData00  
[00  !
$str00! 1
]001 2
=003 4
createdUserResponse005 H
.00H I
Message00I P
;00P Q
TempData11  
[11  !
$str11! (
]11( )
=11* +
createdUserResponse11, ?
.11? @
User11@ D
.11D E
email11E J
;11J K
return22 
RedirectToAction22 /
(22/ 0
$str220 E
)22E F
;22F G
}33 
else44 
{55 
TempData66  
[66  !
$str66! /
]66/ 0
=661 2
createdUserResponse663 F
.66F G
Message66G N
;66N O
}77 
}88 
catch99 
(99 
	Exception99  
ex99! #
)99# $
{:: 
TempData;; 
[;; 
$str;; +
];;+ ,
=;;- .
ex;;/ 1
.;;1 2
Message;;2 9
;;;9 :
}<< 
}== 
return?? 
View?? 
(?? 
newUser?? 
)??  
;??  !
}@@ 	
publicEE 
ActionResultEE 
VerifyAndCreateUserEE /
(EE/ 0
)EE0 1
{FF 	
ViewBagGG 
.GG 
EmailGG 
=GG 
TempDataGG $
[GG$ %
$strGG% ,
]GG, -
;GG- .
returnHH 
ViewHH 
(HH 
)HH 
;HH 
}II 	
[KK 	
HttpPostKK	 
]KK 
publicLL 
asyncLL 
TaskLL 
<LL 
ActionResultLL &
>LL& '
VerifyAndCreateUserLL( ;
(LL; <
stringLL< B
emailLLC H
,LLH I
stringLLJ P
codeLLQ U
)LLU V
{MM 	
varNN 
proxy_serviceNN 
=NN 
newNN  #
	ProxyUserNN$ -
(NN- .
)NN. /
;NN/ 0
tryPP 
{QQ 
varRR 
responseRR 
=RR 
awaitRR $
proxy_serviceRR% 2
.RR2 3
VerifyAndCreateUserRR3 F
(RRF G
emailRRG L
,RRL M
codeRRN R
)RRR S
;RRS T
ifTT 
(TT 
responseTT 
!=TT 
nullTT  $
&&TT% '
responseTT( 0
.TT0 1
MessageTT1 8
!=TT9 ;
nullTT< @
)TT@ A
{UU 
returnVV 
JsonVV 
(VV  
newVV  #
{VV$ %
MessageVV& -
=VV. /
responseVV0 8
.VV8 9
MessageVV9 @
}VVA B
)VVB C
;VVC D
}WW 
elseXX 
{YY 
returnZZ 
JsonZZ 
(ZZ  
newZZ  #
{ZZ$ %
MessageZZ& -
=ZZ. /
$strZZ0 N
}ZZO P
)ZZP Q
;ZZQ R
}[[ 
}\\ 
catch]] 
(]] 
	Exception]] 
ex]] 
)]]  
{^^ 
return__ 
Json__ 
(__ 
new__ 
{__  !
Message__" )
=__* +
$"__, .
$str__. L
{__L M
ex__M O
.__O P
Message__P W
}__W X
"__X Y
}__Z [
)__[ \
;__\ ]
}`` 
}aa 	
publicee 
ActionResultee 
Loginee !
(ee! "
)ee" #
{ff 	
returngg 
Viewgg 
(gg 
)gg 
;gg 
}hh 	
[jj 	
HttpPostjj	 
]jj 
[kk 	$
ValidateAntiForgeryTokenkk	 !
]kk! "
publicll 
asyncll 
Taskll 
<ll 
ActionResultll &
>ll& '
Loginll( -
(ll- .
stringll. 4
emailll5 :
,ll: ;
stringll< B
passwordllC K
)llK L
{mm 	
varnn 
proxy_servicenn 
=nn 
newnn  #
	ProxyUsernn$ -
(nn- .
)nn. /
;nn/ 0
ifpp 
(pp 
stringpp 
.pp 
IsNullOrWhiteSpacepp )
(pp) *
emailpp* /
)pp/ 0
||pp1 3
stringpp4 :
.pp: ;
IsNullOrWhiteSpacepp; M
(ppM N
passwordppN V
)ppV W
)ppW X
{qq 
TempDatarr 
[rr 
$strrr '
]rr' (
=rr) *
$strrr+ R
;rrR S
returnss 
Viewss 
(ss 
)ss 
;ss 
}tt 
tryvv 
{ww 
varxx 
responsexx 
=xx 
awaitxx $
proxy_servicexx% 2
.xx2 3
Loginxx3 8
(xx8 9
emailxx9 >
,xx> ?
passwordxx@ H
)xxH I
;xxI J
ifzz 
(zz 
responsezz 
.zz 
Successzz $
)zz$ %
{{{ 
TempData|| 
[|| 
$str|| -
]||- .
=||/ 0
response||1 9
.||9 :
Message||: A
;||A B
TempData}} 
[}} 
$str}} $
]}}$ %
=}}& '
response}}( 0
.}}0 1
Email}}1 6
;}}6 7
return~~ 
RedirectToAction~~ +
(~~+ ,
$str~~, =
)~~= >
;~~> ?
} 
else
�� 
{
�� 
TempData
�� 
[
�� 
$str
�� +
]
��+ ,
=
��- .
response
��/ 7
.
��7 8
Message
��8 ?
;
��? @
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
�� 
TempData
�� 
[
�� 
$str
�� '
]
��' (
=
��) *
ex
��+ -
.
��- .
Message
��. 5
;
��5 6
}
�� 
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
public
�� 
ActionResult
�� 
VerifyLoginCode
�� +
(
��+ ,
)
��, -
{
�� 	
ViewBag
�� 
.
�� 
Email
�� 
=
�� 
TempData
�� $
[
��$ %
$str
��% ,
]
��, -
;
��- .
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '
VerifyLoginCode
��( 7
(
��7 8
string
��8 >
email
��? D
,
��D E
string
��F L
code
��M Q
)
��Q R
{
�� 	
var
�� 
proxy_service
�� 
=
�� 
new
��  #
	ProxyUser
��$ -
(
��- .
)
��. /
;
��/ 0
if
�� 
(
�� 
string
�� 
.
��  
IsNullOrWhiteSpace
�� )
(
��) *
code
��* .
)
��. /
)
��/ 0
{
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
Message
��" )
=
��* +
$str
��, ?
}
��@ A
)
��A B
;
��B C
}
�� 
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
�� $
proxy_service
��% 2
.
��2 3

VerifyCode
��3 =
(
��= >
email
��> C
,
��C D
code
��E I
)
��I J
;
��J K
if
�� 
(
�� 
response
�� 
!=
�� 
null
��  $
&&
��% '
!
��( )
string
��) /
.
��/ 0
IsNullOrEmpty
��0 =
(
��= >
response
��> F
.
��F G
Token
��G L
)
��L M
)
��M N
{
�� 
TempData
�� 
[
�� 
$str
�� -
]
��- .
=
��/ 0
$str
��1 L
;
��L M
TempData
�� 
[
�� 
$str
�� $
]
��$ %
=
��& '
email
��( -
;
��- .
return
�� 
Json
�� 
(
��  
new
��  #
{
��$ %
Message
��& -
=
��. /
response
��0 8
.
��8 9
Message
��9 @
,
��@ A
Token
��B G
=
��H I
response
��J R
.
��R S
Token
��S X
}
��Y Z
)
��Z [
;
��[ \
}
�� 
else
�� 
{
�� 
return
�� 
Json
�� 
(
��  
new
��  #
{
��$ %
Message
��& -
=
��. /
$str
��0 O
}
��P Q
)
��Q R
;
��R S
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
�� 
return
�� 
Json
�� 
(
�� 
new
�� 
{
��  !
Message
��" )
=
��* +
$"
��, .
$str
��. L
{
��L M
ex
��M O
.
��O P
Message
��P W
}
��W X
"
��X Y
}
��Z [
)
��[ \
;
��\ ]
}
�� 
}
�� 	
[
�� 	
HttpGet
��	 
]
�� 
public
�� 
async
�� 
Task
�� 
<
�� 
ActionResult
�� &
>
��& '
GetAllUsers
��( 3
(
��3 4
)
��4 5
{
�� 	
var
�� 
proxy_service
�� 
=
�� 
new
��  #
	ProxyUser
��$ -
(
��- .
)
��. /
;
��/ 0
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
�� $
proxy_service
��% 2
.
��2 3
GetAllUsers
��3 >
(
��> ?
)
��? @
;
��@ A
if
�� 
(
�� 
response
�� 
.
�� 
Success
�� $
)
��$ %
{
�� 
return
�� 
View
�� 
(
��  
response
��  (
.
��( )
Users
��) .
)
��. /
;
��/ 0
}
�� 
else
�� 
{
�� 
TempData
�� 
[
�� 
$str
�� +
]
��+ ,
=
��- .
response
��/ 7
.
��7 8
Message
��8 ?
;
��? @
return
�� 
View
�� 
(
��  
)
��  !
;
��! "
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
�� 
TempData
�� 
[
�� 
$str
�� '
]
��' (
=
��) *
ex
��+ -
.
��- .
Message
��. 5
;
��5 6
return
�� 
View
�� 
(
�� 
)
�� 
;
�� 
}
�� 
}
�� 	
public
�� 
ActionResult
�� 

DeleteUser
�� &
(
��& '
int
��' *
id
��+ -
)
��- .
{
�� 	
var
�� 
proxy_service
�� 
=
�� 
new
��  #
	ProxyUser
��$ -
(
��- .
)
��. /
;
��/ 0
try
�� 
{
�� 
var
�� 
response
�� 
=
�� 
proxy_service
�� ,
.
��, -

DeleteUser
��- 7
(
��7 8
id
��8 :
)
��: ;
;
��; <
if
�� 
(
�� 
response
�� 
!=
�� 
null
��  $
&&
��% '
response
��( 0
.
��0 1
Success
��1 8
)
��8 9
{
�� 
TempData
�� 
[
�� 
$str
�� -
]
��- .
=
��/ 0
$str
��1 S
;
��S T
}
�� 
else
�� 
{
�� 
TempData
�� 
[
�� 
$str
�� +
]
��+ ,
=
��- .
response
��/ 7
?
��7 8
.
��8 9
Message
��9 @
??
��A C
$str
��D v
;
��v w
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
�� 
TempData
�� 
[
�� 
$str
�� '
]
��' (
=
��) *
$"
��+ -
$str
��- K
{
��K L
ex
��L N
.
��N O
Message
��O V
}
��V W
"
��W X
;
��X Y
}
�� 
return
�� 
RedirectToAction
�� #
(
��# $
$str
��$ 1
)
��1 2
;
��2 3
}
�� 	
public
�� 
ActionResult
�� 
EditUser
�� $
(
��$ %
int
��% (
id
��) +
)
��+ ,
{
�� 	
var
�� 
proxyService
�� 
=
�� 
new
�� "
	ProxyUser
��# ,
(
��, -
)
��- .
;
��. /
var
�� 
userResponse
�� 
=
�� 
proxyService
�� +
.
��+ ,
GetUserById
��, 7
(
��7 8
id
��8 :
)
��: ;
;
��; <
if
�� 
(
�� 
!
�� 
userResponse
�� 
.
�� 
Success
�� %
)
��% &
{
�� 
TempData
�� 
[
�� 
$str
�� '
]
��' (
=
��) *
userResponse
��+ 7
.
��7 8
Message
��8 ?
;
��? @
return
�� 
RedirectToAction
�� '
(
��' (
$str
��( 5
)
��5 6
;
��6 7
}
�� 
return
�� 
View
�� 
(
�� 
userResponse
�� $
.
��$ %
User
��% )
)
��) *
;
��* +
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
public
�� 
ActionResult
�� 
EditUser
�� $
(
��$ %
int
��% (
id
��) +
,
��+ ,
User
��- 1
updatedUser
��2 =
)
��= >
{
�� 	
var
�� 
proxyService
�� 
=
�� 
new
�� "
	ProxyUser
��# ,
(
��, -
)
��- .
;
��. /
try
�� 
{
�� 
var
�� 
response
�� 
=
�� 
proxyService
�� +
.
��+ ,

UpdateUser
��, 6
(
��6 7
id
��7 9
,
��9 :
updatedUser
��; F
)
��F G
;
��G H
if
�� 
(
�� 
response
�� 
.
�� 
Success
�� $
)
��$ %
{
�� 
TempData
�� 
[
�� 
$str
�� -
]
��- .
=
��/ 0
response
��1 9
.
��9 :
Message
��: A
;
��A B
return
�� 
RedirectToAction
�� +
(
��+ ,
$str
��, 9
)
��9 :
;
��: ;
}
�� 
else
�� 
{
�� 
TempData
�� 
[
�� 
$str
�� +
]
��+ ,
=
��- .
response
��/ 7
.
��7 8
Message
��8 ?
;
��? @
return
�� 
View
�� 
(
��  
updatedUser
��  +
)
��+ ,
;
��, -
}
�� 
}
�� 
catch
�� 
(
�� 
	Exception
�� 
ex
�� 
)
��  
{
�� 
TempData
�� 
[
�� 
$str
�� '
]
��' (
=
��) *
$"
��+ -
$str
��- 4
{
��4 5
ex
��5 7
.
��7 8
Message
��8 ?
}
��? @
"
��@ A
;
��A B
return
�� 
View
�� 
(
�� 
updatedUser
�� '
)
��' (
;
��( )
}
�� 
}
�� 	
}
�� 
}�� �
�D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\Controllers\HomeController.cs
	namespace 	
Presentation
 
. 
Controllers "
{ 
public		 

class		 
HomeController		 
:		  !

Controller		" ,
{

 
public 
ActionResult 
Index !
(! "
)" #
{ 	
var 
email 
= 
TempData  
[  !
$str! (
]( )
as* ,
string- 3
;3 4
ViewBag 
. 
Email 
= 
email !
;! "
return 
View 
( 
) 
; 
} 	
public 
ActionResult 
About !
(! "
)" #
{ 	
ViewBag 
. 
Message 
= 
$str B
;B C
return 
View 
( 
) 
; 
} 	
public 
ActionResult 
Contact #
(# $
)$ %
{ 	
ViewBag 
. 
Message 
= 
$str 2
;2 3
return 
View 
( 
) 
; 
} 	
}   
}!! �	
}D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\App_Start\RouteConfig.cs
	namespace 	
Presentation
 
{		 
public

 

class

 
RouteConfig

 
{ 
public 
static 
void 
RegisterRoutes )
() *
RouteCollection* 9
routes: @
)@ A
{ 	
routes 
. 
IgnoreRoute 
( 
$str ;
); <
;< =
routes 
. 
MapRoute 
( 
name 
: 
$str 
,  
url 
: 
$str 1
,1 2
defaults 
: 
new 
{ 

controller  *
=+ ,
$str- 3
,3 4
action5 ;
=< =
$str> E
,E F
idG I
=J K
UrlParameterL X
.X Y
OptionalY a
}b c
) 
; 
} 	
} 
} �
~D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\App_Start\FilterConfig.cs
	namespace 	
Presentation
 
{ 
public 

class 
FilterConfig 
{ 
public 
static 
void !
RegisterGlobalFilters 0
(0 1"
GlobalFilterCollection1 G
filtersH O
)O P
{		 	
filters

 
.

 
Add

 
(

 
new

  
HandleErrorAttribute

 0
(

0 1
)

1 2
)

2 3
;

3 4
} 	
} 
} �
~D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\Presentation\App_Start\BundleConfig.cs
	namespace 	
Presentation
 
{ 
public 

class 
BundleConfig 
{ 
public		 
static		 
void		 
RegisterBundles		 *
(		* +
BundleCollection		+ ;
bundles		< C
)		C D
{

 	
bundles 
. 
Add 
( 
new 
ScriptBundle (
(( )
$str) ;
); <
.< =
Include= D
(D E
$str 7
)7 8
)8 9
;9 :
bundles 
. 
Add 
( 
new 
ScriptBundle (
(( )
$str) >
)> ?
.? @
Include@ G
(G H
$str 4
)4 5
)5 6
;6 7
bundles 
. 
Add 
( 
new 
ScriptBundle (
(( )
$str) >
)> ?
.? @
Include@ G
(G H
$str /
)/ 0
)0 1
;1 2
bundles 
. 
Add 
( 
new 
Bundle "
(" #
$str# 8
)8 9
.9 :
Include: A
(A B
$str .
). /
)/ 0
;0 1
bundles 
. 
Add 
( 
new 
StyleBundle '
(' (
$str( 7
)7 8
.8 9
Include9 @
(@ A
$str /
,/ 0
$str *
)* +
)+ ,
;, -
bundles 
. 
Add 
( 
new 
StyleBundle '
(' (
$str( <
)< =
.= >
Include> E
(E F
$str 4
)4 5
)5 6
;6 7
bundles 
. 
Add 
( 
new 
StyleBundle '
(' (
$str( A
)A B
.B C
IncludeC J
(J K
$str 0
)0 1
)1 2
;2 3
bundles   
.   
Add   
(   
new   
StyleBundle   '
(  ' (
$str  ( E
)  E F
.  F G
Include  G N
(  N O
$str!! A
)!!A B
)!!B C
;!!C D
bundles"" 
."" 
Add"" 
("" 
new"" 
StyleBundle"" '
(""' (
$str""( =
)""= >
.""> ?
Include""? F
(""F G
$str## 0
)##0 1
)##1 2
;##2 3
}$$ 	
}%% 
}&& 