console.log("This script is properly referenced.");

    $('#contactForm').submit(function (e) {
        e.preventDefault();
        console.log("before contact form");
        var data = {
            name: $('#contactName').val(),
            email: $('#contactEmail').val(),
            subject: $('#contactSubject').val(),
            message: $('#contactMessage').val()
        }

        console.log(data);
        $("#btnSubmitForm").html("<i class = 'fa fa-spinner fa-spin'></i> Please Wait").css('margin', '10px');
        $("#btnSubmitForm").attr("disabled", "disabled");
        //var sLoader = $('#submit-loader');
        console.log("in contact form");

        $.ajax({
        type: "POST",
            url: "/Home/SendEmail",
            data: data,
            
            success: function (response) {
                console.log(response)
                if (response.statusCode == "00") {
                    swal("Thank you", "I would get back to you shortly", "success");
                    $("#btnSubmitForm").removeAttr("disabled");
                    $("#btnSubmitForm").html("Send Message");
                    $('#contactName').val(""),
                    $('#contactEmail').val(""),
                    $('#contactSubject').val(""),
                    $('#contactMessage').val("")
                    console.log('dfsghgdg fgdg dfgdgf dfgdgf ddf');
                }
                else {
                    swal("An error occurred", "Something went wrong. Please try again.", "error");
                    console.log('Something went wrong. Please try again!!.');
                    $("#btnSubmitForm").removeAttr("disabled");
                    $("#btnSubmitForm").html("Send Message");
                  
                }

            },
            error: function () {

                    console.log('Something went wrong. Please try again!!.');

            }
        });


    })

