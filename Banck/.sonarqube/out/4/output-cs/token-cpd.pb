þ
dD:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\SL\Class1.cs
	namespace 	
SL
 
{ 
public 

class 
Class1 
{ 
} 
}		 úX
vD:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\SL\Authorization\JwtService.cs
	namespace 	
SL
 
. 
Authorization 
{ 
public 

static 
class 

JwtService "
{ 
private 
const 
string 
	SecretKey &
=' (
$str) V
;V W
private 
const 
int "
TokenExpirationMinutes 0
=1 2
$num3 5
;5 6
private 
const 
string 
Issuer #
=$ %
$str& 4
;4 5
private 
const 
string 
Audience %
=& '
$str( 6
;6 7
public 
static 
string 
GenerateToken *
(* +
string+ 1
email2 7
,7 8
string9 ?
role@ D
)D E
{ 	
try 
{ 
var 
key 
= 
Encoding "
." #
UTF8# '
.' (
GetBytes( 0
(0 1
	SecretKey1 :
): ;
;; <
var 
credentials 
=  !
new" %
SigningCredentials& 8
(8 9
new  
SymmetricSecurityKey ,
(, -
key- 0
)0 1
,1 2
SecurityAlgorithms &
.& '

HmacSha256' 1
) 
; 
var   
claims   
=   
new    
[    !
]  ! "
{!! 
new"" 
Claim"" 
("" 

ClaimTypes"" (
.""( )
Email"") .
,"". /
email""0 5
)""5 6
,""6 7
new## 
Claim## 
(## 

ClaimTypes## (
.##( )
Role##) -
,##- .
role##/ 3
)##3 4
,##4 5
new$$ 
Claim$$ 
($$ 
System$$ $
.$$$ %
IdentityModel$$% 2
.$$2 3
Tokens$$3 9
.$$9 :
Jwt$$: =
.$$= >#
JwtRegisteredClaimNames$$> U
.$$U V
Jti$$V Y
,$$Y Z
Guid$$[ _
.$$_ `
NewGuid$$` g
($$g h
)$$h i
.$$i j
ToString$$j r
($$r s
)$$s t
)$$t u
,$$u v
}%% 
;%% 
var'' 
token'' 
='' 
new'' 
JwtSecurityToken''  0
(''0 1
issuer(( 
:(( 
Issuer(( "
,((" #
audience)) 
:)) 
Audience)) &
,))& '
claims** 
:** 
claims** "
,**" #
expires++ 
:++ 
DateTime++ %
.++% &
UtcNow++& ,
.++, -

AddMinutes++- 7
(++7 8"
TokenExpirationMinutes++8 N
)++N O
,++O P
signingCredentials,, &
:,,& '
credentials,,( 3
)-- 
;-- 
return// 
new// #
JwtSecurityTokenHandler// 2
(//2 3
)//3 4
.//4 5

WriteToken//5 ?
(//? @
token//@ E
)//E F
;//F G
}00 
catch11 
(11 
	Exception11 
ex11 
)11  
{22 
return44 
null44 
;44 
}55 
}66 	
public99 
static99 
string99 
GetRoleFromToken99 -
(99- .
string99. 4
token995 :
)99: ;
{:: 	
try;; 
{<< 
var== 
tokenHandler==  
===! "
new==# &#
JwtSecurityTokenHandler==' >
(==> ?
)==? @
;==@ A
var>> 
key>> 
=>> 
Encoding>> "
.>>" #
UTF8>># '
.>>' (
GetBytes>>( 0
(>>0 1
	SecretKey>>1 :
)>>: ;
;>>; <
var@@  
validationParameters@@ (
=@@) *
new@@+ .%
TokenValidationParameters@@/ H
{AA $
ValidateIssuerSigningKeyBB ,
=BB- .
trueBB/ 3
,BB3 4
IssuerSigningKeyCC $
=CC% &
newCC' * 
SymmetricSecurityKeyCC+ ?
(CC? @
keyCC@ C
)CCC D
,CCD E
ValidateIssuerDD "
=DD# $
trueDD% )
,DD) *
ValidIssuerEE 
=EE  !
IssuerEE" (
,EE( )
ValidateAudienceFF $
=FF% &
trueFF' +
,FF+ ,
ValidAudienceGG !
=GG" #
AudienceGG$ ,
,GG, -
ValidateLifetimeHH $
=HH% &
trueHH' +
,HH+ ,
	ClockSkewII 
=II 
TimeSpanII  (
.II( )
ZeroII) -
}JJ 
;JJ 
varLL 
	principalLL 
=LL 
tokenHandlerLL  ,
.LL, -
ValidateTokenLL- :
(LL: ;
tokenLL; @
,LL@ A 
validationParametersLLB V
,LLV W
outLLX [
_LL\ ]
)LL] ^
;LL^ _
varMM 
	roleClaimMM 
=MM 
	principalMM  )
.MM) *
ClaimsMM* 0
.MM0 1
FirstOrDefaultMM1 ?
(MM? @
cMM@ A
=>MMB D
cMME F
.MMF G
TypeMMG K
==MML N

ClaimTypesMMO Y
.MMY Z
RoleMMZ ^
)MM^ _
;MM_ `
returnNN 
	roleClaimNN  
?NN  !
.NN! "
ValueNN" '
;NN' (
}OO 
catchPP 
(PP 
	ExceptionPP 
exPP 
)PP  
{QQ 
returnSS 
nullSS 
;SS 
}TT 
}UU 	
publicXX 
staticXX 
boolXX 
ValidateTokenXX (
(XX( )
stringXX) /
tokenXX0 5
)XX5 6
{YY 	
tryZZ 
{[[ 
var\\ 
tokenHandler\\  
=\\! "
new\\# &#
JwtSecurityTokenHandler\\' >
(\\> ?
)\\? @
;\\@ A
var]] 
key]] 
=]] 
Encoding]] "
.]]" #
UTF8]]# '
.]]' (
GetBytes]]( 0
(]]0 1
	SecretKey]]1 :
)]]: ;
;]]; <
var__  
validationParameters__ (
=__) *
new__+ .%
TokenValidationParameters__/ H
{`` $
ValidateIssuerSigningKeyaa ,
=aa- .
trueaa/ 3
,aa3 4
IssuerSigningKeybb $
=bb% &
newbb' * 
SymmetricSecurityKeybb+ ?
(bb? @
keybb@ C
)bbC D
,bbD E
ValidateIssuercc "
=cc# $
truecc% )
,cc) *
ValidIssuerdd 
=dd  !
Issuerdd" (
,dd( )
ValidateAudienceee $
=ee% &
trueee' +
,ee+ ,
ValidAudienceff !
=ff" #
Audienceff$ ,
,ff, -
ValidateLifetimegg $
=gg% &
truegg' +
,gg+ ,
	ClockSkewhh 
=hh 
TimeSpanhh  (
.hh( )
Zerohh) -
}ii 
;ii 
tokenHandlerkk 
.kk 
ValidateTokenkk *
(kk* +
tokenkk+ 0
,kk0 1 
validationParameterskk2 F
,kkF G
outkkH K
_kkL M
)kkM N
;kkN O
returnll 
truell 
;ll 
}mm 
catchnn 
{oo 
returnpp 
falsepp 
;pp 
}qq 
}rr 	
publicuu 
staticuu 
stringuu 
GetEmailFromTokenuu .
(uu. /
stringuu/ 5
tokenuu6 ;
)uu; <
{vv 	
tryww 
{xx 
varyy 
tokenHandleryy  
=yy! "
newyy# &#
JwtSecurityTokenHandleryy' >
(yy> ?
)yy? @
;yy@ A
varzz 
keyzz 
=zz 
Encodingzz "
.zz" #
UTF8zz# '
.zz' (
GetByteszz( 0
(zz0 1
	SecretKeyzz1 :
)zz: ;
;zz; <
var||  
validationParameters|| (
=||) *
new||+ .%
TokenValidationParameters||/ H
{}} $
ValidateIssuerSigningKey~~ ,
=~~- .
true~~/ 3
,~~3 4
IssuerSigningKey $
=% &
new' * 
SymmetricSecurityKey+ ?
(? @
key@ C
)C D
,D E
ValidateIssuer
€€ "
=
€€# $
true
€€% )
,
€€) *
ValidIssuer
 
=
  !
Issuer
" (
,
( )
ValidateAudience
‚‚ $
=
‚‚% &
true
‚‚' +
,
‚‚+ ,
ValidAudience
ƒƒ !
=
ƒƒ" #
Audience
ƒƒ$ ,
,
ƒƒ, -
ValidateLifetime
„„ $
=
„„% &
true
„„' +
,
„„+ ,
	ClockSkew
…… 
=
…… 
TimeSpan
……  (
.
……( )
Zero
……) -
}
†† 
;
†† 
var
ˆˆ 
	principal
ˆˆ 
=
ˆˆ 
tokenHandler
ˆˆ  ,
.
ˆˆ, -
ValidateToken
ˆˆ- :
(
ˆˆ: ;
token
ˆˆ; @
,
ˆˆ@ A"
validationParameters
ˆˆB V
,
ˆˆV W
out
ˆˆX [
_
ˆˆ\ ]
)
ˆˆ] ^
;
ˆˆ^ _
var
‰‰ 

emailClaim
‰‰ 
=
‰‰  
	principal
‰‰! *
.
‰‰* +
Claims
‰‰+ 1
.
‰‰1 2
FirstOrDefault
‰‰2 @
(
‰‰@ A
c
‰‰A B
=>
‰‰C E
c
‰‰F G
.
‰‰G H
Type
‰‰H L
==
‰‰M O

ClaimTypes
‰‰P Z
.
‰‰Z [
Email
‰‰[ `
)
‰‰` a
;
‰‰a b
return
ŠŠ 

emailClaim
ŠŠ !
?
ŠŠ! "
.
ŠŠ" #
Value
ŠŠ# (
;
ŠŠ( )
}
‹‹ 
catch
ŒŒ 
{
 
return
ŽŽ 
null
ŽŽ 
;
ŽŽ 
}
 
}
 	
public
““ 
static
““ 
DateTime
““ 
?
““ $
GetTokenExpirationTime
““  6
(
““6 7
string
““7 =
token
““> C
)
““C D
{
”” 	
try
•• 
{
–– 
var
—— 
tokenHandler
——  
=
——! "
new
——# &%
JwtSecurityTokenHandler
——' >
(
——> ?
)
——? @
;
——@ A
var
˜˜ 
jwtToken
˜˜ 
=
˜˜ 
tokenHandler
˜˜ +
.
˜˜+ ,
	ReadToken
˜˜, 5
(
˜˜5 6
token
˜˜6 ;
)
˜˜; <
as
˜˜= ?
JwtSecurityToken
˜˜@ P
;
˜˜P Q
return
™™ 
jwtToken
™™ 
?
™™  
.
™™  !
ValidTo
™™! (
;
™™( )
}
šš 
catch
›› 
{
œœ 
return
 
null
 
;
 
}
žž 
}
ŸŸ 	
}
   
}¡¡ Ò	
{D:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\SL\Authentication\PasswordHasher.cs
	namespace 	
SL
 
. 
Authentication 
{ 
public 

class 
PasswordHasher 
{ 
public

 
static

 
string

 
HashPassword

 )
(

) *
string

* 0
password

1 9
)

9 :
{ 	
return 
BCrypt 
. 
Net 
. 
BCrypt $
.$ %
HashPassword% 1
(1 2
password2 :
): ;
;; <
} 	
public 
static 
bool 
VerifyPassword )
() *
string* 0
password1 9
,9 :
string; A
hashedPasswordB P
)P Q
{ 	
return 
BCrypt 
. 
Net 
. 
BCrypt $
.$ %
Verify% +
(+ ,
password, 4
,4 5
hashedPassword6 D
)D E
;E F
} 	
} 
} Â
zD:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\SL\Authentication\IEmailService.cs
	namespace 	
SL
 
. 
Authentication 
{ 
public 

	interface 
IEmailService "
{		 
Task 
SendEmailAsync 
( 
string "
recipientEmail# 1
,1 2
string3 9
subject: A
,A B
stringC I
bodyJ N
)N O
;O P
} 
} ³
yD:\7. Septimo Semestre\7. Desarrollo de Software Seguro\TERCER PARCIAL\BankSystem\Banck\SL\Authentication\EmailService.cs
	namespace 	
SL
 
. 
Authentication 
{		 
public

 

class

 
EmailService

 
:

 
IEmailService

  -
{ 
private 
const 
string 

SmtpServer '
=( )
$str* :
;: ;
private 
const 
int 
SmtpPort "
=# $
$num% (
;( )
private 
const 
string 
SenderEmail (
=) *
$str+ F
;F G
private 
const 
string 
SenderPassword +
=, -
$str. @
;@ A
private 
const 
string 

SenderName '
=( )
$str* @
;@ A
public 
async 
Task 
SendEmailAsync (
(( )
string) /
recipientEmail0 >
,> ?
string@ F
subjectG N
,N O
stringP V
bodyW [
)[ \
{ 	
using 
( 
var 
smtp 
= 
new !

SmtpClient" ,
(, -

SmtpServer- 7
,7 8
SmtpPort9 A
)A B
)B C
{ 
smtp 
. 
Credentials  
=! "
new# &
NetworkCredential' 8
(8 9
SenderEmail9 D
,D E
SenderPasswordF T
)T U
;U V
smtp 
. 
	EnableSsl 
=  
true! %
;% &
var 
mail 
= 
new 
MailMessage *
{ 
From 
= 
new 
MailAddress *
(* +
SenderEmail+ 6
,6 7

SenderName8 B
)B C
,C D
Subject 
= 
subject %
,% &
Body 
= 
body 
,  

IsBodyHtml 
=  
true! %
} 
; 
mail!! 
.!! 
To!! 
.!! 
Add!! 
(!! 
recipientEmail!! *
)!!* +
;!!+ ,
await## 
smtp## 
.## 
SendMailAsync## (
(##( )
mail##) -
)##- .
;##. /
}$$ 
}%% 	
}&& 
}'' 