# **Therania web app**

This will be the official documentation and design ideas of my new pet
project: therania.

The idea came to me as I finished an asp.net core book and I wanted a
project to build to apply what I’ve learned, but I didn’t want to do
something generic like an ecommerce site or a todo app.

My late aunt Rania Shokry was a therapist, she was pretty successful and
she did many workshops and other stuff related to mental health care, so
I decided to make a full blown web therapy website dedicated to her.

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

2.  Competitors

3.  Feature list

4.  Feature implementation details

5.  Tech stack discussion

# 1. Technology stack

this is a list of the tech stack used on the website, this list will be updated when future packages or libraries are used.

## a. Front-end:

1. Blazor

## b. Back-end:

1. ASP.NET Core
2. Web Api
3. EF Core

## c. Database:

1. MS SQL Server

# 2. Tech stack discussion

## front-end framework decision

for the fronend I chose blazor WASM.

### why not a javascript framework (react, vue, angular)?

Although a javascript framework is faster and more mature in terms of ecosystem and libraries, but I just finished learning C# and I want this project to improve my C# skills, so I'll do both front and backend with C#

### blazor WASM vs balzor server

I was going to use blazor server, but it is not a true SPA app (it complies c# code on the server and uses a signalR connection to update the UI),it has higher latency because every user interaction involves a network hop, and scaling is bad because as users increase, you need more server resources(ram, cpu) to maintain each client's signalR connection, so I'll stick to blazor WASM

# 3. competitors

## a. betterhelp.com

the website’s business model is simple:

1.  when you sign up, you answer a survey with many questions about your
    mental health status, your age, gender, religion, etc…

2.  after signing up, a therapist is assigned to you, the website has
    over 20k therapists, so you can register to the website as a patient
    or a therapist.

3.  As part of the signing up, you pay a monthly fee of 60$ to 90$.

4.  You can communicate to the therapist in various ways: text
    messaging, live chatting, audio call, video call.

5.  You can request a therapist change if you don’t like the current
    one.

6.  You can view all therapists

7.  Each therapist has his own page, that includes about me,
    specialities, experience, licenses, reviews, and a work with me
    button.

8.  You can choose to work with a specific therapist, when you do, you
    answer the same survey when you sign up, but you get assigned to the
    therapist that you chose.

9.  The account creation process is not possible without the 60$ payment
    (you have to pay to have an account).

<!-- -->

# 4. Feature list

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

-   The total hours of therapy is added at the end of each month and
    taken from the credit card.

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

-   Will include their billing price

-   Will include reviews from other users

### 4. <u>All therapists page:</u>

-   Will include a list of all therapists, with the ability to search
    and filter by: name, price/hr, specialization, country.

-   will have the ability to choose a therapist which redirects you to
    their page.

## <u>As a Therapist:</u>

At the top of the home page, there will be a join us button, which takes
you to the therapists sign up page.

### <u>Sign up:</u>

-   will include fields for: email, first name, last name, gender, age,
    specialization, education, licenses and certificates, profile image,
    about me, price/hour, credit card info for money transfer in case
    someone booked them,

-   then they will be told that their account is in review because a
    therapist account has to be human verified.

-   Their home page will include a section for coming booking requests
    and whether they accept it or not

-   Once they accept it, the user can chat with the therapist to
    schedule time for meetings.

-   Meetings start up counting the hourly rate of the therapist

-   When the meeting is done, the time of the meeting is saved

-   The total meeting times are summed at the end of the month and
    multiplied by the therapist’s hourly rate, then it is transferred
    from the user to the therapist’s account
