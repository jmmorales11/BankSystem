‘
fC:\Users\ASUS TUF A15\source\repos\bancoNuevo\BankSystem\Banck\Presentation\Properties\AssemblyInfo.cs
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
]##) *…	
ZC:\Users\ASUS TUF A15\source\repos\bancoNuevo\BankSystem\Banck\Presentation\Global.asax.cs
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
} å&
iC:\Users\ASUS TUF A15\source\repos\bancoNuevo\BankSystem\Banck\Presentation\Controllers\UserController.cs
	namespace 	
Presentation
 
. 
Controllers "
{ 
public 

class 
UserController 
:  !

Controller" ,
{ 
private 
readonly 
IEmailService &
_emailService' 4
;4 5
public 
UserController 
( 
) 
{ 	
_emailService 
= 
new 
EmailService  ,
(, -
)- .
;. /
} 	
public 
ActionResult 
Index !
(! "
)" #
{ 	
return 
View 
( 
) 
; 
} 	
public 
ActionResult 

CreateUser &
(& '
)' (
{ 	
return 
View 
( 
) 
; 
} 	
[## 	
HttpPost##	 
]## 
[$$ 	$
ValidateAntiForgeryToken$$	 !
]$$! "
public%% 
ActionResult%% 

CreateUser%% &
(%%& '
User%%' +
newUser%%, 3
)%%3 4
{&& 	
var'' 
proxy_service'' 
='' 
new''  #
proxy''$ )
('') *
)''* +
;''+ ,
if)) 
()) 

ModelState)) 
.)) 
IsValid)) "
)))" #
{** 
try++ 
{,, 
var-- 
createdUserResponse-- +
=--, -
proxy_service--. ;
.--; <
Create--< B
(--B C
newUser--C J
)--J K
;--K L
if// 
(// 
createdUserResponse// +
!=//, .
null/// 3
&&//4 6
createdUserResponse//7 J
.//J K
Message//K R
!=//S U
null//V Z
)//Z [
{00 
TempData11  
[11  !
$str11! 1
]111 2
=113 4
createdUserResponse115 H
.11H I
Message11I P
;11P Q
return22 
RedirectToAction22 /
(22/ 0
$str220 E
)22E F
;22F G
}33 
}44 
catch55 
(55 
	Exception55  
ex55! #
)55# $
{66 
TempData77 
[77 
$str77 +
]77+ ,
=77- .
ex77/ 1
.771 2
Message772 9
;779 :
}88 
}99 
return:: 
View:: 
(:: 
newUser:: 
)::  
;::  !
};; 	
public?? 
ActionResult?? 
VerifyAndCreateUser?? /
(??/ 0
)??0 1
{@@ 	
returnAA 
ViewAA 
(AA 
)AA 
;AA 
}BB 	
[EE 	
HttpPostEE	 
]EE 
publicFF 
asyncFF 
TaskFF 
<FF 
ActionResultFF &
>FF& '
VerifyAndCreateUserFF( ;
(FF; <
stringFF< B
emailFFC H
,FFH I
stringFFJ P
codeFFQ U
)FFU V
{GG 	
varHH 
proxy_serviceHH 
=HH 
newHH  #
proxyHH$ )
(HH) *
)HH* +
;HH+ ,
tryJJ 
{KK 
varMM 
responseMM 
=MM 
awaitMM $
proxy_serviceMM% 2
.MM2 3
VerifyAndCreateUserMM3 F
(MMF G
emailMMG L
,MML M
codeMMN R
)MMR S
;MMS T
ifOO 
(OO 
responseOO 
!=OO 
nullOO  $
&&OO% '
responseOO( 0
.OO0 1
MessageOO1 8
!=OO9 ;
nullOO< @
)OO@ A
{PP 
returnQQ 
JsonQQ 
(QQ  
newQQ  #
{QQ$ %
MessageQQ& -
=QQ. /
responseQQ0 8
.QQ8 9
MessageQQ9 @
}QQA B
)QQB C
;QQC D
}RR 
elseSS 
{TT 
returnUU 
JsonUU 
(UU  
newUU  #
{UU$ %
MessageUU& -
=UU. /
$strUU0 N
}UUO P
)UUP Q
;UUQ R
}VV 
}WW 
catchXX 
(XX 
	ExceptionXX 
exXX 
)XX  
{YY 
returnZZ 
JsonZZ 
(ZZ 
newZZ 
{ZZ  !
MessageZZ" )
=ZZ* +
$"ZZ, .
$strZZ. L
{ZZL M
exZZM O
.ZZO P
MessageZZP W
}ZZW X
"ZZX Y
}ZZZ [
)ZZ[ \
;ZZ\ ]
}[[ 
}\\ 	
}^^ 
}__ ¨	
iC:\Users\ASUS TUF A15\source\repos\bancoNuevo\BankSystem\Banck\Presentation\Controllers\HomeController.cs
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
{ 	
return 
View 
( 
) 
; 
} 	
public 
ActionResult 
About !
(! "
)" #
{ 	
ViewBag 
. 
Message 
= 
$str B
;B C
return 
View 
( 
) 
; 
} 	
public 
ActionResult 
Contact #
(# $
)$ %
{ 	
ViewBag 
. 
Message 
= 
$str 2
;2 3
return 
View 
( 
) 
; 
} 	
} 
} ò
dC:\Users\ASUS TUF A15\source\repos\bancoNuevo\BankSystem\Banck\Presentation\App_Start\RouteConfig.cs
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
} ”
eC:\Users\ASUS TUF A15\source\repos\bancoNuevo\BankSystem\Banck\Presentation\App_Start\FilterConfig.cs
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
} Ô
eC:\Users\ASUS TUF A15\source\repos\bancoNuevo\BankSystem\Banck\Presentation\App_Start\BundleConfig.cs
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
;6 7
} 	
} 
}   