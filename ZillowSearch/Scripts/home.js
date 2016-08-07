$(document).ready(function () {
    var detailDataTemplate = Handlebars.compile($('#data-detail-template').html());
    var form = $('#search-form').children('form');
    var errorSection = form.find('.form-error');
    var searchDetails = $('#search-details');
    
    // Process the hash from the url and auto query
    var processHash = function () {
        if (window.location.hash) {
            var valueSet = false;
            var keyValueArray = window.location.hash.substr(1).split(';');
            for (var i in keyValueArray) {
                var keyValuePair = keyValueArray[i].split('=');
                if (keyValuePair.length == 2) {
                    form.find('[name="' + decodeURIComponent(keyValuePair[0]) + '"]')
                        .val(decodeURIComponent(keyValuePair[1]));
                    valueSet = true;
                }
            }

            if (valueSet) {
                search();
            }
        }
    };

    var search = function () {
        // Clear any previous errors
        errorSection.empty().hide();
        searchDetails.empty();

        // Build Query Data
        var serverData = {
            Address: $.trim(form.find('[name="Address"]').val()),
            CityAndStateOrZipCode: $.trim(form.find('[name="CityAndStateOrZipCode"]').val()),
            IncludeRentEstimate: $.trim(form.find('[name="IncludeRentEstimate"]').val())
        };

        // Append params to url
        var hash = "";
        for (var key in serverData) {
            hash += ';' + encodeURIComponent(key) + "=" + encodeURIComponent(serverData[key]);
        }
        window.location.hash = hash.substr(1);
        
        // Call Server
        $.get('/Home/Search', serverData, function (data, status, xhr) {
            if (data.Success) {
                for (var i in data.Data) {
                    $('#search-details').append(detailDataTemplate(data.Data[i]));
                }
                if (serverData.IncludeRentEstimate == "false") {
                    $('.rent-zestimate-data').hide();
                }
            }
            else {
                var errorList = $('<ul>');
                for (var i in data.Data)
                {
                    errorList.append('<li>' + data.Data[i] + '</li>');
                }
                errorSection.show().html(errorList);
            }
        });
    };
    
    // Bind button submit event / enter press to send query to server
    $('#search-form-submit').on('click', search);
    $('#search-form input,select').keyup(function (event) {
        if (event.keyCode == 13) {
            search();
        }
    });

    // Process hash from url
    processHash();
});