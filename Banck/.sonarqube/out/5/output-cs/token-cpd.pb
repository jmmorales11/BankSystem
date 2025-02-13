�
gC:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Service\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str "
)" #
]# $
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
$str $
)$ %
]% &
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
]##) *�
cC:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Service\Models\EmailRequest.cs
	namespace 	
Service
 
. 
Models 
{ 
public 

class 
EmailRequest 
{		 
public

 
string

 
Email

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
} 
} �
[C:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Service\Global.asax.cs
	namespace 	
Service
 
{		 
public

 

class

 
WebApiApplication

 "
:

# $
System

% +
.

+ ,
Web

, /
.

/ 0
HttpApplication

0 ?
{ 
	protected 
void 
Application_Start (
(( )
)) *
{ 	
GlobalConfiguration 
.  
	Configure  )
() *
WebApiConfig* 6
.6 7
Register7 ?
)? @
;@ A
} 	
} 
} �Z
jC:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Service\Controllers\UserController.cs
	namespace 	
Service
 
. 
Controllers 
{ 
public 

class 
UserController 
:  !
ApiController" /
{ 
private 
static 

Dictionary !
<! "
string" (
,( )
string* 0
>0 1
VerificationCodes2 C
=D E
newF I

DictionaryJ T
<T U
stringU [
,[ \
string] c
>c d
(d e
)e f
;f g
private 
readonly 
IEmailService &
_emailService' 4
;4 5
public 
UserController 
( 
) 
{ 	
_emailService 
= 
new 
EmailService  ,
(, -
)- .
;. /
} 	
private 
static 

Dictionary !
<! "
string" (
,( )
User* .
>. / 
PendingRegistrations0 D
=E F
newG J

DictionaryK U
<U V
stringV \
,\ ]
User^ b
>b c
(c d
)d e
;e f
["" 	
HttpPost""	 
]"" 
public## 
async## 
Task## 
<## 
IHttpActionResult## +
>##+ ,

CreateUser##- 7
(##7 8
[##8 9
FromBody##9 A
]##A B
User##C G
newUser##H O
)##O P
{$$ 	
var%% 
BL%% 
=%% 
new%% 
	UserLogic%% "
(%%" #
)%%# $
;%%$ %
try'' 
{(( 
BL** 
.** 1
%validateExistingUserAndPasswordSecure** 8
(**8 9
newUser**9 @
)**@ A
;**A B
string-- 
verificationCode-- '
=--( )
new--* -
Random--. 4
(--4 5
)--5 6
.--6 7
Next--7 ;
(--; <
$num--< B
,--B C
$num--D J
)--J K
.--K L
ToString--L T
(--T U
)--U V
;--V W
VerificationCodes00 !
[00! "
newUser00" )
.00) *
email00* /
]00/ 0
=001 2
verificationCode003 C
;00C D 
PendingRegistrations11 $
[11$ %
newUser11% ,
.11, -
email11- 2
]112 3
=114 5
newUser116 =
;11= >
string44 
subject44 
=44  
$str44! G
;44G H
string55 
body55 
=55 
$"55  
$str55  F
{55F G
verificationCode55G W
}55W X
$str55X a
"55a b
;55b c
await66 
_emailService66 #
.66# $
SendEmailAsync66$ 2
(662 3
newUser663 :
.66: ;
email66; @
,66@ A
subject66B I
,66I J
body66K O
)66O P
;66P Q
var99 
response99 
=99 
new99 " 
UserCreationResponse99# 7
{:: 
User;; 
=;; 
newUser;; "
,;;" #
Message<< 
=<< 
$str	<< �
}== 
;== 
return?? 
Ok?? 
(?? 
response?? "
)??" #
;??# $
}@@ 
catchAA 
(AA 
	ExceptionAA 
exAA 
)AA  
{BB 
returnCC 

BadRequestCC !
(CC! "
exCC" $
.CC$ %
MessageCC% ,
)CC, -
;CC- .
}DD 
}EE 	
[II 	
HttpPostII	 
]II 
publicJJ 
IHttpActionResultJJ  
VerifyAndCreateUserJJ! 4
(JJ4 5
[JJ5 6
FromBodyJJ6 >
]JJ> ?
VerifyCodeRequestJJ@ Q
verifyRequestJJR _
)JJ_ `
{KK 	
varLL 
BLLL 
=LL 
newLL 
	UserLogicLL "
(LL" #
)LL# $
;LL$ %
ifOO 
(OO 
VerificationCodesOO !
.OO! "
TryGetValueOO" -
(OO- .
verifyRequestOO. ;
.OO; <
EmailOO< A
,OOA B
outOOC F
stringOOG M

storedCodeOON X
)OOX Y
&&OOZ \

storedCodeOO] g
==OOh j
verifyRequestOOk x
.OOx y
CodeOOy }
)OO} ~
{PP 
VerificationCodesRR !
.RR! "
RemoveRR" (
(RR( )
verifyRequestRR) 6
.RR6 7
EmailRR7 <
)RR< =
;RR= >
ifUU 
(UU  
PendingRegistrationsUU (
.UU( )
TryGetValueUU) 4
(UU4 5
verifyRequestUU5 B
.UUB C
EmailUUC H
,UUH I
outUUJ M
UserUUN R
newUserUUS Z
)UUZ [
)UU[ \
{VV 
varXX 
createdUserXX #
=XX$ %
BLXX& (
.XX( )
CreateXX) /
(XX/ 0
newUserXX0 7
)XX7 8
;XX8 9 
PendingRegistrations[[ (
.[[( )
Remove[[) /
([[/ 0
verifyRequest[[0 =
.[[= >
Email[[> C
)[[C D
;[[D E
return]] 
Ok]] 
(]] 
new]] !
{]]" #
Message]]$ +
=]], -
$str]]. L
,]]L M
User]]N R
=]]S T
createdUser]]U `
}]]a b
)]]b c
;]]c d
}^^ 
else__ 
{`` 
returnaa 
Contentaa "
(aa" #
HttpStatusCodeaa# 1
.aa1 2
NotFoundaa2 :
,aa: ;
newaa< ?
{aa@ A
MessageaaB I
=aaJ K
$str	aaL �
}
aa� �
)
aa� �
;
aa� �
}bb 
}cc 
elsedd 
{ee 
returnff 
Contentff 
(ff 
HttpStatusCodeff -
.ff- .
Unauthorizedff. :
,ff: ;
newff< ?
{ff@ A
MessageffB I
=ffJ K
$strffL y
}ffz {
)ff{ |
;ff| }
}gg 
}hh 	
[ll 	
HttpPostll	 
]ll 
publicmm 
asyncmm 
Taskmm 
<mm 
IHttpActionResultmm +
>mm+ ,
Loginmm- 2
(mm2 3
[mm3 4
FromBodymm4 <
]mm< =
LoginRequestmm> J
loginRequestmmK W
)mmW X
{nn 	
varoo 
BLoo 
=oo 
newoo 
	UserLogicoo "
(oo" #
)oo# $
;oo$ %
stringpp 
recipientEmailpp !
=pp" #
loginRequestpp$ 0
.pp0 1
Emailpp1 6
;pp6 7
stringqq 
subject1qq 
=qq 
$strqq 6
;qq6 7
stringrr 
body1rr 
;rr 
trytt 
{uu 
varww 
userww 
=ww 
BLww 
.ww 
Authenticateww *
(ww* +
loginRequestww+ 7
.ww7 8
Emailww8 =
,ww= >
loginRequestww? K
.wwK L
PasswordwwL T
)wwT U
;wwU V
ifyy 
(yy 
useryy 
==yy 
nullyy  
)yy  !
{zz 
return{{ 
Content{{ "
({{" #
HttpStatusCode{{# 1
.{{1 2
Unauthorized{{2 >
,{{> ?
new{{@ C
{{{D E
Message{{F M
={{N O
$str{{P g
}{{h i
){{i j
;{{j k
}|| 
string 
verificationCode '
=( )
new* -
Random. 4
(4 5
)5 6
.6 7
Next7 ;
(; <
$num< B
,B C
$numD J
)J K
.K L
ToStringL T
(T U
)U V
;V W
VerificationCodes
�� !
[
��! "
loginRequest
��" .
.
��. /
Email
��/ 4
]
��4 5
=
��6 7
verificationCode
��8 H
;
��H I
body1
�� 
=
�� 
$"
�� 
$str
�� @
{
��@ A
verificationCode
��A Q
}
��Q R
$str
��R [
"
��[ \
;
��\ ]
await
�� 
_emailService
�� #
.
��# $
SendEmailAsync
��$ 2
(
��2 3
recipientEmail
��3 A
,
��A B
subject1
��C K
,
��K L
body1
��M R
)
��R S
;
��S T
return
�� 
Ok
�� 
(
�� 
new
�� 
{
�� 
Message
��  '
=
��( )
$str��* �
}��� �
)��� �
;��� �
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
�� !
InternalServerError
�� *
(
��* +
ex
��+ -
)
��- .
;
��. /
}
�� 
}
�� 	
[
�� 	
HttpPost
��	 
]
�� 
public
�� 
IHttpActionResult
��  

VerifyCode
��! +
(
��+ ,
[
��, -
FromBody
��- 5
]
��5 6
VerifyCodeRequest
��7 H
verifyRequest
��I V
)
��V W
{
�� 	
if
�� 
(
�� 
VerificationCodes
�� !
.
��! "
TryGetValue
��" -
(
��- .
verifyRequest
��. ;
.
��; <
Email
��< A
,
��A B
out
��C F
string
��G M

storedCode
��N X
)
��X Y
&&
��Z \

storedCode
��] g
==
��h j
verifyRequest
��k x
.
��x y
Code
��y }
)
��} ~
{
�� 
string
�� 
userRole
�� 
=
��  !
$str
��" (
;
��( )
var
�� 
token
�� 
=
�� 

JwtService
�� &
.
��& '
GenerateToken
��' 4
(
��4 5
verifyRequest
��5 B
.
��B C
Email
��C H
,
��H I
userRole
��J R
)
��R S
;
��S T
return
�� 
Ok
�� 
(
�� 
new
�� 
{
�� 
Token
�� 
=
�� 
token
�� !
,
��! "
Email
�� 
=
�� 
verifyRequest
�� )
.
��) *
Email
��* /
,
��/ 0
Role
�� 
=
�� 
userRole
�� #
,
��# $
Message
�� 
=
�� 
$str
�� -
}
�� 
)
�� 
;
�� 
}
�� 
else
�� 
{
�� 
return
�� 
Content
�� 
(
�� 
HttpStatusCode
�� -
.
��- .
Unauthorized
��. :
,
��: ;
new
��< ?
{
��@ A
Message
��B I
=
��J K
$str
��L y
}
��z {
)
��{ |
;
��| }
}
�� 
}
�� 	
}
�� 
}�� �(
jC:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Service\Controllers\LoanController.cs
	namespace 	
Service
 
. 
Controllers 
{ 
public 

class 
LoanController 
:  !
ApiController" /
{ 
[ 	
HttpPost	 
] 
public 
IHttpActionResult  

CreateLoan! +
(+ ,
[, -
FromBody- 5
]5 6
Loan7 ;
newLoan< C
)C D
{ 	
var 
	loanLogic 
= 
new 
	LoanLogic  )
() *
)* +
;+ ,
try 
{ 
var 
createdLoan 
=  !
	loanLogic" +
.+ ,
Create, 2
(2 3
newLoan3 :
): ;
;; <
return 
Ok 
( 
new 
{ 
Message  '
=( )
$str* H
,H I
LoanJ N
=O P
createdLoanQ \
}] ^
)^ _
;_ `
} 
catch 
( 
	Exception 
ex 
)  
{ 
return 

BadRequest !
(! "
ex" $
.$ %
Message% ,
), -
;- .
} 
} 	
[!! 	
HttpGet!!	 
]!! 
public"" 
IHttpActionResult""  
GetAllLoans""! ,
("", -
)""- .
{## 	
var$$ 
	loanLogic$$ 
=$$ 
new$$ 
	LoanLogic$$  )
($$) *
)$$* +
;$$+ ,
try%% 
{&& 
var'' 
loans'' 
='' 
	loanLogic'' %
.''% &
RetrieveAllLoan''& 5
(''5 6
)''6 7
;''7 8
return(( 
Ok(( 
((( 
new(( 
{(( 
Loans((  %
=((& '
loans((( -
}((. /
)((/ 0
;((0 1
})) 
catch** 
(** 
	Exception** 
ex** 
)**  
{++ 
return,, 
InternalServerError,, *
(,,* +
ex,,+ -
),,- .
;,,. /
}-- 
}.. 	
[11 	
HttpGet11	 
]11 
public22 
IHttpActionResult22  
GetLoanById22! ,
(22, -
int22- 0
id221 3
)223 4
{33 	
var44 
	loanLogic44 
=44 
new44 
	LoanLogic44  )
(44) *
)44* +
;44+ ,
try55 
{66 
var77 
loan77 
=77 
	loanLogic77 $
.77$ %
RetrieveByIdLoan77% 5
(775 6
id776 8
)778 9
;779 :
if88 
(88 
loan88 
==88 
null88  
)88  !
{99 
return:: 
Content:: "
(::" #
HttpStatusCode::# 1
.::1 2
NotFound::2 :
,::: ;
new::< ?
{::@ A
Message::B I
=::J K
$str::L d
}::e f
)::f g
;::g h
};; 
return<< 
Ok<< 
(<< 
new<< 
{<< 
Loan<<  $
=<<% &
loan<<' +
}<<, -
)<<- .
;<<. /
}== 
catch>> 
(>> 
	Exception>> 
ex>> 
)>>  
{?? 
return@@ 
InternalServerError@@ *
(@@* +
ex@@+ -
)@@- .
;@@. /
}AA 
}BB 	
[EE 	

HttpDeleteEE	 
]EE 
publicFF 
IHttpActionResultFF  

DeleteLoanFF! +
(FF+ ,
intFF, /
idFF0 2
)FF2 3
{GG 	
varHH 
	loanLogicHH 
=HH 
newHH 
	LoanLogicHH  )
(HH) *
)HH* +
;HH+ ,
tryII 
{JJ 
boolKK 
deletedKK 
=KK 
	loanLogicKK (
.KK( )
DeleteKK) /
(KK/ 0
idKK0 2
)KK2 3
;KK3 4
ifLL 
(LL 
!LL 
deletedLL 
)LL 
{MM 
returnNN 
ContentNN "
(NN" #
HttpStatusCodeNN# 1
.NN1 2
NotFoundNN2 :
,NN: ;
newNN< ?
{NN@ A
MessageNNB I
=NNJ K
$strNNL z
}NN{ |
)NN| }
;NN} ~
}OO 
returnQQ 
OkQQ 
(QQ 
newQQ 
{QQ 
MessageQQ  '
=QQ( )
$strQQ* K
}QQL M
)QQM N
;QQN O
}RR 
catchSS 
(SS 
	ExceptionSS 
exSS 
)SS  
{TT 
returnUU 
InternalServerErrorUU *
(UU* +
exUU+ -
)UU- .
;UU. /
}VV 
}WW 	
}XX 
}YY �
fC:\Users\ASUS TUF A15\source\repos\Taller1Seguridad\BankSystem\Banck\Service\App_Start\WebApiConfig.cs
	namespace 	
Service
 
{ 
public 

static 
class 
WebApiConfig $
{		 
public

 
static

 
void

 
Register

 #
(

# $
HttpConfiguration

$ 5
config

6 <
)

< =
{ 	
config 
. "
MapHttpAttributeRoutes )
() *
)* +
;+ ,
config 
. 
Routes 
. 
MapHttpRoute &
(& '
name 
: 
$str "
," #
routeTemplate 
: 
$str ?
,? @
defaults 
: 
new 
{ 
id  "
=# $
RouteParameter% 3
.3 4
Optional4 <
}= >
) 
; 
} 	
} 
} 