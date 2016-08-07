Enclosed you will find my simple project that uses and displays the Zillow GetSearchResults API.

Below are a few comments on the design:
    Given the intent that this is a simple project, I made all of the classes with public properties and simply placed them in the Models folder.  Given a production system, this likely wouldn't be the case.
    Unity and Constructor injection were used in order to maintain unit testability.  Some unit tests were written that were used during my testing so that I didn't hit the Zillow service too many times.
    I kept the libraries for the front end to a minimum.  No UI libraries were used, only libraries used were jQuery and handlebars.
	
Additional Comments:
    During my testing, I did find out that if the api is unable to find an address, it returns error code 508, which is not listed on the API detail page.
	
Thanks,
   Kevin Keenum
   kevin.keenum@gmail.com
