$(document).ready(function () {
    var detailDataTemplate = Handlebars.compile($('#data-detail-template').html());

    var search = function () {
        var form = $('#search-form').children('form');
        var errorSection = form.find('.form-error');
        var searchDetails = $('#search-details');

        // Clear any previous errors
        errorSection.empty().hide();
        searchDetails.empty();

        // Call server
        var serverData = {
            Address: form.find('[name="Address"]').val(),
            CityAndStateOrZipCode: form.find('[name="CityAndStateOrZipCode"]').val(),
            IncludeRentEstimate: form.find('[name="IncludeRentEstimate"]').val()
        };

        $.ajaxSetup({ cache: false });
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
    
    // Bind submit event to post data to server
    $('#search-form-submit').on('click', search);
});