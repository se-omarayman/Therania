# **Therania**

This will be the official documentation and design ideas of my new pet
project: therania.

The idea came to me as I finished an asp.net core book and I wanted a
project to build to apply what I’ve learned, but I didn’t want to do
something generic like an ecommerce site or a todo app.

My late aunt Rania Shokry was a therapist, she was pretty successful and
she did many workshops and other stuff related to mental health care, so
I decided to make a full web therapy website dedicated to her.

the app will start with some basic functionality, and will grow as I get
more ideas, this documentation will list everything related to the web
app including: design decisions, feature implementations, stack and
technology decisions (mainly Microsoft stack).

I don’t expect this app to go public or commercial or anything, it’s
just a pet project to apply programming concepts that I’ve learned,
although maybe in the future I can think of going commercial.

I didn’t do any research regarding the market of online therapy
websites, but in Egypt, I don’t think there are online therapy services,
so if I decide to go commercial, the main idea would be to make therapy
accessible from everywhere to anyone and with an affordable price.

# Table of contents

1.  Technology stack

2.  folder structure/navigation

3.  Tech stack discussion

4.  Inspiration

5.  Feature list

6.  Feature implementation details

# 1. Technology stack

this is a list of the tech stack used on the website, this list will be updated when future packages or libraries are used.

1. ASP.Net Core MVC
2. EF Core
3. MS SQL Server
4. Identity for authentication
5. xUnit for unit tests

Note: At first, I decided to separate the project into 2 projects: frontend and backend.
The frontend was going to be made with Blazor WASM, the backend with asp.net core, Web Api, ef core.
Then I realized that doing SPA with a Api backend will add unnecessary complexity to the project because:

1. the backend api will only serve the frontend design, no mobile app or any other design, so combining back and front is better
2. I won't use any complicated SPA javascript features, so doing spa will bloat the project

So, for this project, I will be using asp.net core mvc.

# 2. Folder structure/navigation

```
└───Therania
    ├───src
    │   └───Therania
    │       ├───Controllers
    │       ├───Data
    │       ├───Migrations
    │       ├───Models
    │       ├───Properties
    │       ├───Views
    │       └───wwwroot
    └───tests
        └───TheraniaTests

```

# 3. Tech stack discussion

# 4. Inspiration

## a. betterhelp.com

# 5. Feature list

This section lists the features of the website, I’m not going to
implement them all immediately, so some of them will be left for future implementations.

## <u>As a user:</u>

### 1. <u>Sign up/login</u>:

-   Users can create an account with email, first name, last name,
    gender, age, country, and subscription to newsletter.

-   There will also be optional fields like medical history, therapy
    knowledge etc..

-   A confirmation email will be sent to verify that they used a valid
    email.

-   New accounts don’t require payment as there will be free services.

-   You can login/signup with Google and Facebook

-   A payment method has to be added before booking a therapist

-   the user pays a monthly subscribtion fee if he wants to book a therapist

### 2. <u>Home page:</u>

-   The home page includes a marketing / design carousel (one that says
    something like: “get help now”, “we’re here to help” etc.

-   Will include the services we provide: text chatting, voice, audio,
    etc

-   Will include a list of chosen therapists

-   Will include a choose random therapist functionality.

### 3. <u>Therapist page:</u>

-   Will include a summary/about me section

-   Will include the therapist’s profile photo

-   Win include their specialization

-   Will include reviews from other users

### 4. <u>All therapists page:</u>

-   Will include a list of all therapists, with the ability to search
    and filter by: name, specialization, country.

-   will have the ability to choose a therapist which redirects you to
    their page.

## <u>As a Therapist:</u>

At the top of the home page, there will be a join us button, which takes
you to the therapists sign up page.

### <u>Sign up:</u>

-   will include fields for: email, first name, last name, gender, age,
    specialization, education, licenses and certificates, profile image,
    about me, credit card info for money transfer in case
    someone booked them,

-   then they will be told that their account is in review because a
    therapist account has to be human verified.

-   Their home page will include a section for coming booking requests
    and whether they accept it or not

-   Once they accept it, the user can chat with the therapist to
    schedule time for meetings.

-   When the meeting is done, the time of the meeting is saved in hours

-   the therapist gets a percentage of the subscription fees that the users that booked him payed (if 5 users book with the therapist, he gets a percentage for each user)
