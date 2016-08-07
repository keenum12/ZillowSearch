Enclosed you will find my simple project that uses and displays the Zillow GetSearchResults API.

Here are a few comments on the design:
    Given the intent that this is a simple project, I made all of the classes with public properties and simply placed them in the Models folder.  Given a production system, this wouldn't be the case.
    Unity and Constructor injection were used in order to maintain unit testability.  Some tests are written that were used so I didn't hit the Zillow service to many times.
    I kept the libraries for the front end to a minimum.  No UI libraries were used, only libraries used were jQuery and handlebars.

Thanks,
   -Kevin Keenum
