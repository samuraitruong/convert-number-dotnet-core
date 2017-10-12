# SIMPLE CURRENCY READ SERVICE


## Requirements

This application to implement a simple currency read service with .NET. which allow client application send a request with a valid decimal number and return a string representation of that number in the human reading format

Example : 

Input: 123.45

Output: ONE HUNDRED AND TWENTY-FIVE DOLLAR AND FOURTY-FIVE CENTS

## Solutions
I come with a simple approach. each number will have 2 part before decimal and after the decimal. The number after decimal is Cents and before is Dollars amount
```
Example: 123.45$
Part1: 123$
part2 : 45 
```

The reading number will become simple by reading the chunks of 3 digits then add the unit after those 3 digits.

``` Read(part1) + DOLLARS + Read(part2) +CENTS ```
To read Part1, we split full number to multiple chunks, each chunk has 3 digits and we start in reverse order of number because we want to read from small unit first.  In this example I used a string function to split chunks, we can also use Linq or just a simple using divide and module to get the chunk number. The reason I want to use string because it will help to process a very big number that exceeds MaxValue of Int46 (ulong). 

After we have a chunk of 3 digits, use be logic below to read 3 digits to string.

we define an array to mapping number to reading a word we need 3 arrays as below


```csharp
string[] NUMBERS = new string[] { "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN" };

string[] TYS = new string[] { "", "TWENTY", "THIRTY", "FOURTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

string[] TEENS = new string[] { "", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
        
```
### Reading 3 digit number
If the number <= 10 , just return NUMBERS[number] + unit. for example Read(9, " Cents") => NUMBERS[9] +unit = NINE CENTS

If the number < 20 , just return TEENS[number] + unit. for example Read(9, " Cents") => TEENS[19] +unit = NINTEEN CENTS

IF Number >=20 and <100, function will return TYS[number/10] +"-" + NUMBER[number%10] + unit

`Example : 45 = TYS[4] +"-" + NUMBERS[5] = "FOURTY-FIVE" + UNIT (DOLLARS, CENT, ...)

And for all those number >=100 the rules is return the number of firt digit + apply the same logic for 2&3 digit. 
```csharp
return NUMBERS[number/100] " HUNDRED " + " AND "+ ReadNumber(number%100,"") + Unit
```
`example :345 = NUMBERS[3] + ReadNumber(45) = THREE HUNDRED AND FOURTY-FIVE DOLLARS 

When the input is zero the ReadNumber will return string empty in that case we don't concac it to the end result.

`Example: 400 = NUMBERS[4] + " HUNDRED " (We do not add AND here because 00 return empty.)

```csharp
public string ReadNumber(int number, string unit="") {
            if (number == 0) return string.Empty;
            if (number <= 10) return NUMBERS[number]  + unit;
            
            if(number< 20) return TEENS[number-10]  + unit;
            int ten = number % 10;
            int ty = number / 10;

            if (number < 100) {
                return string.Format(ten == 0?"{0}":"{0}-{1}", TYS[ty-1], NUMBERS[ten]) + unit;
            }
            int hundres = number / 100;
            string secondPart = ReadNumber(number % 100);

            return NUMBERS[hundres] + " HUNDRED" + (string.IsNullOrEmpty(secondPart)?"":" AND " + secondPart) + unit;
        }
```
### Reading big number

Chunks is equipvalent to group of 
```csharp
string[] GROUPS = new string[] {" DOLLARS", " THOUSAND", " MILLION", " BILLION", " TRILLION"};

```
After split the chunk to 3 digit, we will call ReadNumber(chunk, group). we do this in a loop until finish all the chunks and concac the number together. There also few logic check to ignore empty chunk such as "000" .
```csharp
int pos = fullNumberStr.Length;
int chunks = 0;
string results = "";
int chunkValue = 0;
while(pos > 0) {
    if(chunkValue>0) {
        results = " AND " + results;
    }

    string chunk = fullNumberStr.Substring(Math.Max(pos - 3, 0), Math.Min(pos, 3));
    chunkValue = int.Parse(chunk);
    string readChunk = ReadNumber(chunkValue, GROUPS[chunks]);
    pos -= 3;

    if (chunkValue ==0 && pos>0 && chunks ==0) {
        readChunk = GROUPS[chunks];
    }
    chunks++;
    results = readChunk + results;
}
```

The final end string will be this full number concat with cents amount

```csharp
 if (string.IsNullOrEmpty(results)) return readCents;
return results + (string.IsNullOrEmpty(readCents)?"" : (" AND " + readCents));
```

# Test

The application shipped with a xUnit test which covert many cases to test number from 1 cents to 999 Trillion dollars

The test project can be run from visual studio or by dotnet test command
```bash
dotnet test akqa-codechallenge/akqa-codechallenge.test.csproj

```

# Build & Run
This application using .NET Core 2.0, If you are a developer with Visual Studio 2017 installed, just need to open and run it as a normal project. 

If you don't have visual studio, Please download .NET Core SDK to build and run it

after clone source to your local machine, please run below command
```bash
cd akqa-codechallenge
dotnet restore 
dotnet run

```
# API Service

The backend is MVC Web API which can be used by any client application using Http POST request

EndPoint :$HOST/api/v1/converter
## Request Format: 
```json
{
    "InputNumber": 123.123
}
```
Content-Type: Application/json

## Response
Service will return below error code
### 400 - BAD REQUEST 
This error return when you send invalid format, invalid number, or using the wrong decimal symbol. 
### 416 Media not supported
This error return when content type is not set to application/json 
### Method not support 
Then using the wrong method, This API only support HTTP Post
### 500 Internal error
Return when the server failed to read your valid input number. this will happen when the input >=1000 Trillion. API V2 will add support for bigger number later.
### 200 SUCCESSFUL
This is return when server accepts the request and successful to process data. The response body is json format of object look like
```json
{"number":123456789.0,"read":"ONE HUNDRED AND TWENTY-THREE MILLION AND FOUR HUNDRED AND FIFTY-SIX THOUSAND AND SEVEN HUNDRED AND EIGHTY-NINE DOLLARS","success":true}
```

```
read: the human read string of your input
number: your original number rounded to 2 decimal place
```

# Knowns Issue

There is an issue with a singular, for example, it always read Cents and Dollars for just 1 cent or 1 dollar. This is not hard to fix. I may fix it later

# Contact

If you encounter any issue, contact me samuraitruong@hotmail.com

