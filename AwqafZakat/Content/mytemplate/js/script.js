// ImageShow script
function ShowImagePreview(imageUploader, previewImage) {
    if (imageUploader.files && imageUploader.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(previewImage).attr('src', e.target.result);
        }
        reader.readAsDataURL(imageUploader.files[0]);
    }
}
/* Add row onclick */
function onAddRowClick(event) {
    $('table.memberdetails tbody tr:last').eq(0).clone().insertBefore($('table.memberdetails tbody tr:last')).show();
    //$('input[name=familymembername],input[name=memberCivilNumber],input[name=relativerelation],input[name=case],select[name="educationLevelID"],input[name=educationalInstitute],input[name=memberNotes]').off('change');
    // $('input[name=familymembername],input[name=memberCivilNumber],input[name=relativerelation],input[name=case],select[name="educationLevelID"],input[name=educationalInstitute],input[name=memberNotes]').on();
}







$(document).ready(function () {
    /* Hijri Date picker */
    $("#registrationDateHijri").datepicker({
        dateFormat: 'yy-mm-dd',
        beforeShow: addhijridate,
        onChangeMonthYear: addhijridate,
        beforeShowDay: function (date) {
            var title = "" + getwriteIslamicDate(0, date, "day")
            return [true, "", title];
        }
    });
    addhijridate();
    $('.addrow').on('click', onAddRowClick);
    /* Date picker */
    $("#registrationDate,#transferDate,#deletionDate,#nextUpdationDate").datepicker({ dateFormat: 'yy-mm-dd' }).data('datepicker');
    /* Horizontal tab for family Registration */
    $('#horizontalTab').easyResponsiveTabs({
        type: 'default', //Types: default, vertical, accordion
        width: 'auto', //auto or any width like 600px
        fit: true   // 100% fit in a container
    });
    /* radio oncheck subsidy state */
    $("#receiveSubsidy").click(function () {
        if ($(this).is(":checked")) {
            $("#subsidystate").show();
        } else {
            $("#subsidystate").hide();
        }
    });

    /* calculate total income and expense */
    $('input.incomeamount').on('keyup', function () {
        var add = 0;
        $(".incomeamount").each(function () {
            add += Number($(this).val());
        });
        $("#totalincome").text(add);
        $(".totalHouseholdIncome").val(add);
    });
    $('input.expenseamount').on('keyup', function () {
        var add = 0;
        $(".expenseamount").each(function () {
            add += Number($(this).val());
        });
        $("#totalexpense").text(add);
        $(".totalHouseholdExpense").val(add);
    });

    /* Age calculator */
    $("#dob").datepicker({
        changeMonth: true,
        changeYear: true,
        showAnim: 'slideDown',
        dateFormat: 'yy-mm-dd'
    }).on('change', function () {
        var age = getAge(this);
        $('#age').val(age);
        // console.log(age);
        //alert(age);

    });

    function getAge(dateVal) {
        var
            birthday = new Date(dateVal.value),
            today = new Date(),
            ageInMilliseconds = new Date(today - birthday),
            years = ageInMilliseconds / (24 * 60 * 60 * 1000 * 365.25),
            months = 12 * (years % 1),
            days = Math.floor(30 * (months % 1));
        // return Math.floor(years) + ' years ' + Math.floor(months) + ' months ' + days + ' days';
        return Math.floor(years);

    }
    /* Age calculator end */


    /* ajax search civilnumber or registration number */
    $("#searchBtn").click(function (e) {
        e.stopPropagation();
        e.preventDefault();

        var searchBy = $("#searchBy").val();
        // alert(searchBy);
        var searchValue = $("#txtSearch").val();
        // alert(searchValue);
        // var setData = $(".form-horizontal");
        // setData.html("");
        $.ajax({
            type: "post",
            url: "/FamilyDeletions/GetSearchingData?searchBy=" + searchBy + "&searchValue=" + searchValue,
            // contentType: "html",
            contentType: "json",
            success: function (result) {
                if (result.length > 0) {
                    $.each(result, function (index, value) {
                        // alert(value.registrationDateHijri);
                        // $("#registrationDateHijri").text(value.registrationDateHijri);
                        $("#registrationDateHijri").val(value.registrationDateHijri);
                        $("#familyRegID").val(value.familyRegID);
                        $("#civilNumber").val(value.civilNumber);
                        $("#area").val(value.region);
                        $("#railNumber").val(value.railwayNumber);
                        $("#homeNumber").val(value.homePhone);
                        $("#oldHomeNumber").val(value.houseNumber);
                        $("#nearestMosque").val(value.mosque);
                        $("#familyIncomeAtReg").val(value.familyPerCapitaIncome);
                        $("#nearestMosque").val(value.mosque);



                        $("#ecoordinate").val(value.ecoordinate);
                        $("#ncoordinate").val(value.ncoordinate);
                        $("#tripleNameTribe").val(value.tripleNameTribe);
                        $("#oldRegion").val(value.region);
                        $("#oldVillage").val(value.village);
                        $("#oldRailNumber").val(value.railwayNumber);
                        $("#oldHomeNumber").val(value.houseNumber);
                        $("#oldMosque").val(value.mosque);
                        $("#oldMobile").val(value.homePhone);
                        $("#oldTelephone").val(value.officePhone);
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Invalid ID number");
                $("#registrationDateHijri").val("");
                $("#familyRegID").val("");
                $("#civilNumber").val("");
                $("#area").val("");
                $("#railNumber").val("");
                $("#homeNumber").val("");
                $("#nearestMosque").val("");
                $("#familyIncomeAtReg").val("");
                $("#nearestMosque").val("");


                $("#ecoordinate").val("");
                $("#ncoordinate").val("");
                $("#tripleNameTribe").val("");
                $("#oldRegion").val("");
                $("#oldVillage").val("");
                $("#oldRailNumber").val("");
                $("#oldHomeNumber").val("");
                $("#oldMosque").val("");
                $("#oldMobile").val("");
                $("#oldTelephone").val("");
            }



        });
    });

    /* Report printing by ajax search civilnumber or registration */
    $("#searchReport").click(function (e) {
        e.stopPropagation();
        e.preventDefault();

        var searchBy = $("#searchBy").val();
        // alert(searchBy);
        var searchValue = $("#txtSearch").val();
        // alert(searchValue);
        // var setData = $(".form-horizontal");
        // setData.html("");
        $.ajax({
            type: "post",
            url: "/Reports/GetSearchingData?searchBy=" + searchBy + "&searchValue=" + searchValue,
            contentType: "html",
            // contentType: "json",
            success: function (result) {
                if (result.length > 0) {
                    $.each(result, function (index, value) {
                        // alert(value.registrationDateHijri);
                        // $("#registrationDateHijri").text(value.registrationDateHijri);
                        $(".allRegistered").hide();
                        $("#registrationDateHijri").val(value.registrationDateHijri);
                        $("#familyRegID").val(value.familyRegID);
                        $("#civilNumber").val(value.civilNumber);
                        $("#area").val(value.region);
                        $("#railNumber").val(value.railwayNumber);
                        $("#homeNumber").val(value.homePhone);
                        $("#oldHomeNumber").val(value.houseNumber);
                        $("#nearestMosque").val(value.mosque);
                        $("#familyIncomeAtReg").val(value.familyPerCapitaIncome);
                        $("#nearestMosque").val(value.mosque);



                        $("#ecoordinate").val(value.ecoordinate);
                        $("#ncoordinate").val(value.ncoordinate);
                        $("#tripleNameTribe").val(value.tripleNameTribe);
                        $("#oldRegion").val(value.region);
                        $("#oldVillage").val(value.village);
                        $("#oldRailNumber").val(value.railwayNumber);
                        $("#oldHomeNumber").val(value.houseNumber);
                        $("#oldMosque").val(value.mosque);
                        $("#oldMobile").val(value.homePhone);
                        $("#oldTelephone").val(value.officePhone);
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Invalid ID number");
                $("#registrationDateHijri").val("");
                $("#familyRegID").val("");
                $("#civilNumber").val("");
                $("#area").val("");
                $("#railNumber").val("");
                $("#homeNumber").val("");
                $("#nearestMosque").val("");
                $("#familyIncomeAtReg").val("");
                $("#nearestMosque").val("");


                $("#ecoordinate").val("");
                $("#ncoordinate").val("");
                $("#tripleNameTribe").val("");
                $("#oldRegion").val("");
                $("#oldVillage").val("");
                $("#oldRailNumber").val("");
                $("#oldHomeNumber").val("");
                $("#oldMosque").val("");
                $("#oldMobile").val("");
                $("#oldTelephone").val("");
            }

        });
    });


    //Smooth Scroll
    $(function () {
        $('a[href*="#"]:not([href="#"])').click(function () {
            if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
                var target = $(this.hash);
                target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
                if (target.length) {
                    $('html, body').animate({
                        scrollTop: target.offset().top
                    }, 1000);
                    return false;
                }
            }
        });
    });


    // Main Menu
    $('#main-nav').affix({
        offset: {
            top: $('header').height()
        }
    });

    $(".navbar-collapse a").on('click', function () {
        $(".navbar-collapse.collapse").removeClass('in');
    });

    // Top Search
    $("#ss").click(function (e) {
        e.preventDefault();
        $(this).toggleClass('current');
        $(".search-form").toggleClass('visible');
    });


    //Slider
    /*$('.flexslider').flexslider({
		animation: "fade",
		directionNav: false,
		pauseOnAction: false,
	});

	var containerPosition = $('.container').offset();
	var positionPad = containerPosition.left + 15;

	$('#slider').find('.caption').css({
		left: positionPad + 'px',
		marginTop: '-' + $('.caption').height() / 2 + 'px'
	});
*/

    // number effect
    $('.about-bg-heading').one('inview', function (event, visible) {
        if (visible == true) {
            $('.count').each(function () {
                $(this).prop('Counter', 0).animate({
                    Counter: $(this).text()
                }, {
                    duration: 5000,
                    easing: 'swing',
                    step: function (now) {
                        $(this).text(Math.ceil(now));
                    }
                });
            });
        }
    });


    //Google Map
    var get_latitude = $('#google-map').data('latitude');
    var get_longitude = $('#google-map').data('longitude');

    function initialize_google_map() {
        var myLatlng = new google.maps.LatLng(get_latitude, get_longitude);
        var mapOptions = {
            zoom: 14,
            scrollwheel: false,
            center: myLatlng
        };
        var map = new google.maps.Map(document.getElementById('google-map'), mapOptions);
        var marker = new google.maps.Marker({
            position: myLatlng,
            map: map
        });
    }
    // google.maps.event.addDomListener(window, 'load', initialize_google_map);


});

