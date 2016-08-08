Enclosed you will find my simple project that uses and displays the Zillow GetSearchResults API.

Below are a few comments on the design:
    Given the intent that this is a simple project only for displaying information, the search result classes all contain public properties and were simply placed in the Models folder.  Given a production system, this likely wouldn't be the case.
    Unity and Constructor injection were used in order to maintain unit testability.  A few unit tests were written that were useful during my testing to avoid hitting the Zillow service too many times.
    I kept the libraries for the front end to a minimum.  No UI libraries were used, only libraries used were jQuery and handlebars.
	
Additional Comments:
    During my testing, I did find out that if the api is unable to find an address, it returns error code 508, which is not listed on the API detail page.
    
Please let me know if you have any questions.
	
Thanks,
   Kevin Keenum
   kevin.keenum@gmail.com
