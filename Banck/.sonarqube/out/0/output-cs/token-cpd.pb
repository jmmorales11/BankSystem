¥
eC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\LoanResponseDto.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class 
LoanResponseDto  
:  !
ResponseDto! ,
{ 
public		 
List		 
<		 
Loan		 
>		 
Loans		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
Loan

 
Loan

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
} 
} ‹
gC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\VerifyCodeRequest.cs
	namespace 	
Entities
 
{ 
public 

class 
VerifyCodeRequest "
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
( )
public 
string 
Code 
{ 
get  
;  !
set" %
;% &
}' (
public 
bool 
Success 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ˜
bC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\LoginRequest.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class 
LoginRequest 
{ 
public		 
string		 
Email		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
string

 
Password

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
bool 
Success 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ¥
eC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\UserResponseDto.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class 
UserResponseDto  
:  !
ResponseDto! ,
{ 
public		 
List		 
<		 
User		 
>		 
Users		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
User

 
User

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
} 
} Û
mC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\ResponseAmortizationDto.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class #
ResponseAmortizationDto (
:( )
ResponseDto) 4
{ 
public		 
Amortization		 
Amortization		 (
{		) *
get		+ .
;		. /
set		0 3
;		3 4
}		5 6
public

 
List

 
<

 !
AmortizationDetailDto

 )
>

) *
AmortizationDetails

+ >
{

? @
get

A D
;

D E
set

F I
;

I J
}

K L
} 
} €
aC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\ResponseDto.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class 
ResponseDto 
{ 
public		 
bool		 
Success		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
string

 
Message

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
} 
} ˜
jC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\UserCreationResponse.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class  
UserCreationResponse %
:% &
ResponseDto& 1
{ 
public		 
User		 
User		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
}

 
} ˛
iC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\UserDataResponseDto.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class 
UserDataResponseDto $
:% &
ResponseDto' 2
{ 
public		 
	User_Data		 
UserData		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
}

 
} ù
cC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\LoginResponse.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class 
LoginResponse 
: 
ResponseDto *
{ 
public		 
string		 
Token		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
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
( )
public 
string 
Role 
{ 
get  
;  !
set" %
;% &
}' (
} 
} —
mC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\AmortizationResponseDto.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 	
class
 #
AmortizationResponseDto '
{ 
public		 
bool		 
Success		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 
string

 
Message

 
{

 
get

  #
;

# $
set

% (
;

( )
}

* +
public 
List 
< 
Amortization  
>  ! 
AmortizationSchedule" 6
{7 8
get9 <
;< =
set> A
;A B
}C D
} 
} â

kC:\Users\ASUS TUF A15\source\repos\DesarrolloSeguro\BankSystem\Banck\Entities\DTOs\AmortizationDetailDto.cs
	namespace 	
Entities
 
. 
DTOs 
{ 
public 

class !
AmortizationDetailDto &
{ 
public		 
int		 
InstallmentNumber		 $
{		% &
get		' *
;		* +
set		, /
;		/ 0
}		1 2
public

 
DateTime

 
PaymentDate

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
public 
decimal 
CapitalAmount $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
decimal 
InterestAmount %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
decimal 
TotalPayment #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 
decimal 
RemainingBalance '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} 