Ž
cC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\DAL\Properties\AssemblyInfo.cs
[ 
assembly 	
:	 

AssemblyTitle 
( 
$str 
) 
]  
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
$str  
)  !
]! "
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
]!!) *Û
WC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\DAL\IRepository.cs
	namespace		 	
DAL		
 
{

 
public 

	interface 
IRepository  
:! "
IDisposable# .
{ 
TEntity 
Create 
< 
TEntity 
> 
(  
TEntity  '
toCreate( 0
)0 1
where2 7
TEntity8 ?
:@ A
classB G
;G H
bool 
Delete 
< 
TEntity 
> 
( 
TEntity $
toDelete% -
)- .
where/ 4
TEntity5 <
:= >
class? D
;D E
bool 
Update 
< 
TEntity 
> 
( 
TEntity $
toUpdate% -
)- .
where/ 4
TEntity5 <
:= >
class? D
;D E
TEntity 
Retrieve 
< 
TEntity  
>  !
(! "

Expression" ,
<, -
Func- 1
<1 2
TEntity2 9
,9 :
bool; ?
>? @
>@ A
criteriaB J
)J K
whereL Q
TEntityR Y
:Z [
class\ a
;a b
List 
< 
TEntity 
> 
Filter 
< 
TEntity $
>$ %
(% &

Expression& 0
<0 1
Func1 5
<5 6
TEntity6 =
,= >
bool? C
>C D
>D E
criteriaF N
)N O
whereP U
TEntityV ]
:^ _
class` e
;e f
List 
< 
TEntity 
> 
RetrieveAll !
<! "
TEntity" )
>) *
(* +
)+ ,
where- 2
TEntity3 :
:; <
class= B
;B C
} 
} ¯=
XC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\DAL\EFRepository.cs
	namespace 	
DAL
 
{ 
public 

class 
EFRepository 
: 
IRepository  +
{ 
	DbContext 

_dbContext 
; 
public 
EFRepository 
( 
	DbContext %
	dbContext& /
)/ 0
{ 	
string 
connectionString #
=$ %"
ConnectionStringHelper& <
.< =
GetConnectionString= P
(P Q
$strQ _
)_ `
;` a

_dbContext 
= 
new 
	DbContext &
(& '
connectionString' 7
)7 8
;8 9

_dbContext 
. 
Configuration $
.$ %
LazyLoadingEnabled% 7
=8 9
false: ?
;? @
} 	
public 
TEntity 
Create 
< 
TEntity %
>% &
(& '
TEntity' .
toCreate/ 7
)7 8
where9 >
TEntity? F
:G H
classI N
{ 	
TEntity 
result 
= 
default $
;$ %
try 
{ 

_dbContext 
. 
Set 
< 
TEntity &
>& '
(' (
)( )
.) *
Add* -
(- .
toCreate. 6
)6 7
;7 8

_dbContext   
.   
SaveChanges   &
(  & '
)  ' (
;  ( )
result!! 
=!! 
toCreate!! !
;!!! "
}"" 
catch## 
(## 
	Exception## 
)## 
{$$ 
throw%% 
;%% 
}&& 
return'' 
result'' 
;'' 
}(( 	
public** 
bool** 
Delete** 
<** 
TEntity** "
>**" #
(**# $
TEntity**$ +
toDelete**, 4
)**4 5
where**6 ;
TEntity**< C
:**D E
class**F K
{++ 	
bool,, 
result,, 
=,, 
false,, 
;,,  
try-- 
{.. 

_dbContext// 
.// 
Entry//  
<//  !
TEntity//! (
>//( )
(//) *
toDelete//* 2
)//2 3
.//3 4
State//4 9
=//: ;
EntityState//< G
.//G H
Deleted//H O
;//O P
result00 
=00 

_dbContext00 #
.00# $
SaveChanges00$ /
(00/ 0
)000 1
>002 3
$num004 5
;005 6
}11 
catch22 
(22 
	Exception22 
)22 
{33 
throw44 
;44 
}55 
return66 
result66 
;66 
}77 	
public99 
void99 
Dispose99 
(99 
)99 
{:: 	
if;; 
(;; 

_dbContext;; 
!=;; 
null;; "
);;" #
{<< 

_dbContext== 
.== 
Dispose== "
(==" #
)==# $
;==$ %
}>> 
}?? 	
publicAA 
ListAA 
<AA 
TEntityAA 
>AA 
FilterAA #
<AA# $
TEntityAA$ +
>AA+ ,
(AA, -

ExpressionAA- 7
<AA7 8
FuncAA8 <
<AA< =
TEntityAA= D
,AAD E
boolAAF J
>AAJ K
>AAK L
criteriaAAM U
)AAU V
whereAAW \
TEntityAA] d
:AAe f
classAAg l
{BB 	
ListCC 
<CC 
TEntityCC 
>CC 
resultCC  
=CC! "
nullCC# '
;CC' (
tryDD 
{EE 
resultFF 
=FF 

_dbContextFF #
.FF# $
SetFF$ '
<FF' (
TEntityFF( /
>FF/ 0
(FF0 1
)FF1 2
.FF2 3
WhereFF3 8
(FF8 9
criteriaFF9 A
)FFA B
.FFB C
ToListFFC I
(FFI J
)FFJ K
;FFK L
}GG 
catchHH 
(HH 
	ExceptionHH 
)HH 
{II 
throwJJ 
;JJ 
}KK 
returnLL 
resultLL 
;LL 
}MM 	
publicOO 
TEntityOO 
RetrieveOO 
<OO  
TEntityOO  '
>OO' (
(OO( )

ExpressionOO) 3
<OO3 4
FuncOO4 8
<OO8 9
TEntityOO9 @
,OO@ A
boolOOB F
>OOF G
>OOG H
criteriaOOI Q
)OOQ R
whereOOS X
TEntityOOY `
:OOa b
classOOc h
{PP 	
TEntityQQ 
resultQQ 
=QQ 
nullQQ !
;QQ! "
tryRR 
{SS 
resultTT 
=TT 

_dbContextTT #
.TT# $
SetTT$ '
<TT' (
TEntityTT( /
>TT/ 0
(TT0 1
)TT1 2
.TT2 3
FirstOrDefaultTT3 A
(TTA B
criteriaTTB J
)TTJ K
;TTK L
}UU 
catchVV 
(VV 
	ExceptionVV 
)VV 
{WW 
throwXX 
;XX 
}YY 
returnZZ 
resultZZ 
;ZZ 
}[[ 	
public]] 
bool]] 
Update]] 
<]] 
TEntity]] "
>]]" #
(]]# $
TEntity]]$ +
toUpdate]], 4
)]]4 5
where]]6 ;
TEntity]]< C
:]]D E
class]]F K
{^^ 	
bool__ 
result__ 
=__ 
false__ 
;__  
try`` 
{aa 

_dbContextbb 
.bb 
Entrybb  
<bb  !
TEntitybb! (
>bb( )
(bb) *
toUpdatebb* 2
)bb2 3
.bb3 4
Statebb4 9
=bb: ;
EntityStatebb< G
.bbG H
ModifiedbbH P
;bbP Q
resultcc 
=cc 

_dbContextcc #
.cc# $
SaveChangescc$ /
(cc/ 0
)cc0 1
>cc2 3
$numcc4 5
;cc5 6
}dd 
catchee 
(ee 
	Exceptionee 
)ee 
{ff 
throwgg 
;gg 
}hh 
returnii 
resultii 
;ii 
}jj 	
publicll 
Listll 
<ll 
TEntityll 
>ll 
RetrieveAllll (
<ll( )
TEntityll) 0
>ll0 1
(ll1 2
)ll2 3
wherell4 9
TEntityll: A
:llB C
classllD I
{mm 	
Listnn 
<nn 
TEntitynn 
>nn 
resultnn  
=nn! "
nullnn# '
;nn' (
tryoo 
{pp 
resultrr 
=rr 

_dbContextrr #
.rr# $
Setrr$ '
<rr' (
TEntityrr( /
>rr/ 0
(rr0 1
)rr1 2
.rr2 3
ToListrr3 9
(rr9 :
)rr: ;
;rr; <
}ss 
catchtt 
(tt 
	Exceptiontt 
extt 
)tt  
{uu 
throwww 
newww 
	Exceptionww #
(ww# $
$strww$ L
,wwL M
exwwN P
)wwP Q
;wwQ R
}xx 
returnyy 
resultyy 
;yy 
}zz 	
}|| 
}}} ø
]C:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\DAL\RepositoryFactory.cs
	namespace 	
DAL
 
{ 
public		 

class		 
RepositoryFactory		 "
{

 
public 
static 
IRepository !
CreateRepository" 2
(2 3
)3 4
{ 	
return 
new 
EFRepository #
(# $
new$ '
Entities( 0
.0 1
BankEntities1 =
(= >
)> ?
)? @
;@ A
} 	
} 
} ²
bC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\DAL\ConnectionStringHelper.cs
	namespace 	
DAL
 
{ 
public 

static 
class "
ConnectionStringHelper .
{ 
public 
static 
string 
GetConnectionString 0
(0 1
string1 7
connectionName8 F
)F G
{		 	
string 
password 
= 
Environment )
.) *"
GetEnvironmentVariable* @
(@ A
$strA N
)N O
;O P
string  
baseConnectionString '
=( ) 
ConfigurationManager* >
.> ?
ConnectionStrings? P
[P Q
connectionNameQ _
]_ `
?` a
.a b
ConnectionStringb r
;r s
if 
( 
string 
. 
IsNullOrEmpty $
($ % 
baseConnectionString% 9
)9 :
): ;
{ 
throw 
new 
	Exception #
(# $
$"$ &
$str& Z
{Z [
connectionName[ i
}i j
$strj }
"} ~
)~ 
;	 €
} 
string #
updatedConnectionString *
=+ , 
baseConnectionString- A
.A B
ReplaceB I
(I J
$strJ b
,b c
passwordd l
)l m
;m n
return #
updatedConnectionString *
;* +
} 	
} 
} 