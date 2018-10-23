var url = '@Url.Action("GetMore", "Home")';
var colorOfDay = '#243973';
var searchHeading = $('#searchOptionsHeading');
var colorInput = $('#colorNumber');
var clearName = $('#clear-name');
var clearNameIcon = $('#clear-name-icon');
var clearNameSm = $('#clear-name-sm');
var clearNameIconSm = $('#clear-name-icon-sm');
var selectedColor = '';
var keywords = $('#keywords-input');
var colorDisplay = $('#results-header');
var socialIcons = $('.social path');
var colorR = $('#colorR');
var colorG = $('#colorG');
var colorB = $('#colorB');
var colorRsm = $('#colorR-sm');
var colorGsm = $('#colorG-sm');
var colorBsm = $('#colorB-sm');
var selectedLabel = $('#current-color');
var selectorBtn = $('.modal-trigger');
var mobileMenuBtn = $('.button-collapse i:not(.landing)');


var searchColor = colorOfDay;
var selectedPaintColor = '';
var foreColor = getForegroundColor(colorOfDay);
var tempColor = $('#color').val() !== '' ? '#' + $('#color').val() : '#ffffff';
var tempForeColor = "";
var mathRound = Math.round;
var mathMin = Math.min;
var mathMax = Math.max;

var colorHiddenInput = $('#color');
var colorNameInput = $('#colorName');
var colorNameHome = $('#colorNameHome');
var keywordsInput = $('#keywords-input');

$('#collapseSearch').on('shown.bs.collapse', function () {
    setColor($(colorHiddenInput).val());

    $('#searchByCustom').click().focus();
    if (window.screen.availHeight < $('#collapseSearch').height()) {
        $('.search-header').css('position', 'static');
    }
})

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results === null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}

$(window).on("popstate", function (e) {
    if ($.urlParam('c') !== null && $.urlParam('c') !== 'null') {
        searchColor = '#' + $.urlParam('c');
    }
    else {
        searchColor = colorOfDay;
    }

    setColor(searchColor);

    if (searchColor === colorOfDay) {
        $('#initial-headings').show();
        propegateColor(false);
    }
    else {
        propegateColor(true);
    }
});

function getInfoCardPositions(count, msgCount) {

    if (count <= 8) { return [count]; }
    else if (count <= 16) { return [Math.round(count / 2), count]; }
    else if (count <= 32) { return [Math.round(count / 3), Math.round(count / 3 * 2), count]; }
    else if (count <= 64) { return [Math.round(count / 4), Math.round(count / 4 * 2), Math.round((count / 4) * 3), count]; }
    else if (count <= 96) { return [Math.round(count / 5), Math.round(count / 5 * 2), Math.round((count / 5) * 3), Math.round((count / 5) * 4), count]; }
    else if (count <= 128) { return [Math.round(count / 6), Math.round(count / 6 * 2), Math.round((count / 6) * 3), Math.round((count / 6) * 4), Math.round((count / 6) * 5), count]; }
    else {
        var positions = [];
        for (var i = 1; i <= msgCount; i++) {
            positions.push(Math.round(count / msgCount) * i)
        }
        return positions;
    }
}

function getInfoCard(msg) {

    var html = '<div class="col s12 m8 l3 prod">' +
                '<div class="card msg-card" style="background-color:' + tempColor + '; color: ' + tempForeColor + '">' +
                    '<div class="card-content">' +
                        '<span class="card-title">' + msg.Title + '</span>' +
                        '<p>' + msg.Message + '</p>' +
                    '</div>';

    if (msg.HasLinks) {
        html += '<div class="card-action">' +
                    '<a href="' + msg.Link + '" target="_blank" style="color: ' + tempForeColor + '">' + msg.LinkText + '</a>' +
                '</div>';
    }

    html += '</div></div>';
    return html;

}

function setColor(newColor) {
    if (newColor.trim().length === 0) { newColor = '#ffffff'; }
    tempColor = newColor;
    tempForeColor = getForegroundColor(newColor);
    colorHiddenInput.val(newColor.replace('#', ''));
    searchHeading.css({ color: tempForeColor });
    colorInput.css({ borderBottomColor: tempForeColor, color: tempForeColor });
    keywords.css({ borderBottomColor: tempForeColor, color: tempForeColor, boxShadow: '0 1px 0 0 ' + tempForeColor });
    //colorInput2.css({ borderBottomColor: tempForeColor, color: tempForeColor });
    colorNameInput.css({ borderBottomColor: tempForeColor, color: tempForeColor, boxShadow: '0 1px 0 0 ' + tempForeColor }).val(getColorName(newColor));
    //colorNameInputSm.css({ borderBottomColor: tempForeColor, color: tempForeColor, boxShadow: '0 1px 0 0 ' + tempForeColor })
    mobileMenuBtn.css({ color: tempForeColor });
    //clearName.css({ backgroundColor: tempColor })
    //clearNameIcon.css({ color: tempForeColor });
    //clearNameSm.css({ backgroundColor: tempColor })
    //clearNameIconSm.css({ color: tempForeColor });
    colorR.css({ borderBottomColor: tempForeColor, color: tempForeColor });
    colorG.css({ borderBottomColor: tempForeColor, color: tempForeColor });
    colorB.css({ borderBottomColor: tempForeColor, color: tempForeColor });
    //colorRsm.css({ borderBottomColor: tempForeColor });
    //colorGsm.css({ borderBottomColor: tempForeColor });
    //colorBsm.css({ borderBottomColor: tempForeColor });
    colorDisplay.css({ backgroundColor: newColor, color: tempForeColor });
    //$('.goBtn').css({ backgroundColor: newColor, color: tempForeColor });
    //if (newColor !== selectedColor) {
    //    colorNameInput.val('');
    //    //colorNameInputSm.val('');
    //}
    $('.input-field span').css('color', tempForeColor);
    $('.rgbLabel').css('color', tempForeColor);
    setRGBVals(newColor);
}

function propegateColor(hideInitialHeadings) {
    searchColor = tempColor;
    foreColor = tempForeColor;
    colorHiddenInput.val(searchColor);
    var tempColorName = getColorName(searchColor);
    
    //$('nav').css({ backgroundColor: searchColor, color: foreColor });
    searchHeading.css('color', foreColor);
    colorInput.css({ color: foreColor });
    keywords.css('color', foreColor);
    //colorInput2.css({ color: foreColor }).text(tempColorName);
    colorNameInput.css({ color: foreColor }).val(tempColorName);
    //colorNameInputSm.css({ color: foreColor }).text(tempColorName);
    clearName.css({ backgroundColor: searchColor });
    clearNameIcon.css({ color: foreColor });
    clearNameSm.css({ backgroundColor: searchColor });
    clearNameIconSm.css({ color: foreColor });
    selectedLabel.css({ color: foreColor }).text(tempColorName);
    mobileMenuBtn.css({ color: foreColor });
    //socialIcons.css({ stroke: foreColor, fill: foreColor });
    if (tempForeColor === "#1e1e1e") {
        var btnBGColor = LightenDarkenColor(searchColor, -40);
        var btnColor = getForegroundColor(btnBGColor);
        $('.search-btn').css({ backgroundColor: btnBGColor, color: btnColor });
    }
    else {
        $('.search-btn').css({ backgroundColor: LightenDarkenColor(searchColor, 40), color: tempForeColor });
    }

    $('.spinner-layer').css({ borderColor: searchColor });

    var rgb = hexToRgb(tempColor);

    if (rgb.r > 230 && rgb.b > 230 && rgb.g > 230) {
        $('#results').empty();
        var html = "<div class='container center-align'><h4>To White!</h4><p>We don't index white or close to white. Try something a little darker</p></div>";
        $(html).appendTo('#results');
        return;
    }

    if (rgb.r < 50 && rgb.b < 50 && rgb.g < 50) {
        $('#results').empty();
        var html1 = "<div class='container center-align'><h4>To Dark!</h4><p>We don't index black or close to black. Try something a little lighter.</p></div>";
        $(html1).appendTo('#results');
        return;

    }
}


$('.goBtn').click(function (e) {
    propegateColor(true);
});

clearName.click(function () {
    colorNameInput.val('');
    colorNameInput.focus();
});

//clearNameSm.click(function () {
//    colorNameInputSm.val('');
//    colorNameInputSm.focus();
//});

$('#colorMfgNameNo').focusin(function () {
    $('#colorMfgNameNo').css('border-bottom-color', tempForeColor).css('box-shadow', '0 1px 0 0 ' + tempForeColor);
});
$('#colorMfgNameNo').focusout(function () {
    $('#colorMfgNameNo').css('box-shadow', '0 1px 0 0 ' + tempForeColor);
});

$('#colorMfgNameNoSm').focusin(function () {
    $('#colorMfgNameNoSm').css('border-bottom-color', tempForeColor).css('box-shadow', '0 1px 0 0 ' + tempForeColor);
});
$('#colorMfgNameNoSm').focusout(function () {
    $('#colorMfgNameNoSm').css('box-shadow', '0 1px 0 0 ' + tempForeColor);
});

//$('#more').click(function (e) {
//    e.preventDefault();
//    GetMore();
//});


function getForegroundColor(hexcolor) {
    var r = parseInt(hexcolor.substr(1, 2), 16);
    var g = parseInt(hexcolor.substr(3, 2), 16);
    var b = parseInt(hexcolor.substr(5, 2), 16);
    var yiq = ((r * 299) + (g * 587) + (b * 114)) / 1000;
    if (yiq >= 128) {
        keywords.removeClass('tt-placeholder-light');
        keywords.addClass('tt-placeholder-dark');
        $('#colorName').removeClass('tt-placeholder-light');
        $('#colorName').addClass('tt-placeholder-dark');
    }
    else {
        keywords.removeClass('tt-placeholder-dark');
        keywords.addClass('tt-placeholder-light');
        $('#colorName').removeClass('tt-placeholder-dark');
        $('#colorName').addClass('tt-placeholder-light');
    }
    return (yiq >= 128) ? '#1e1e1e' : '#f5f5f5';
}

function hexToRgb(hex) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16)
    } : null;
}

// Check to see if string passed in is a percentage
function isPercentage(n) {
    return typeof n === "string" && n.indexOf('%') !== -1;
}

// Need to handle 1.0 as 100%, since once it is a number, there is no difference between it and 1
// <http://stackoverflow.com/questions/7422072/javascript-how-to-detect-number-as-a-decimal-including-1-0>
function isOnePointZero(n) {
    return typeof n === "string" && n.indexOf('.') !== -1 && parseFloat(n) === 1;
}

// Take input from [0, n] and return it as [0, 1]
function bound01(n, max) {
    if (isOnePointZero(n)) { n = "100%"; }

    var processPercent = isPercentage(n);
    n = mathMin(max, mathMax(0, parseFloat(n)));

    // Automatically convert percentage into number
    if (processPercent) {
        n = parseInt(n * max, 10) / 100;
    }

    // Handle floating point rounding errors
    if ((Math.abs(n - max) < 0.000001)) {
        return 1;
    }

    // Convert into [0, 1] range if it isn't already
    return (n % max) / parseFloat(max);
}

function getHue(r, g, b) {

    r = bound01(r, 255);
    g = bound01(g, 255);
    b = bound01(b, 255);

    var max = mathMax(r, g, b), min = mathMin(r, g, b);
    var h, s, l = (max + min) / 2;

    if (max === min) {
        h = s = 0; // achromatic
    }
    else {
        var d = max - min;
        s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
        switch (max) {
            case r: h = (g - b) / d + (g < b ? 6 : 0); break;
            case g: h = (b - r) / d + 2; break;
            case b: h = (r - g) / d + 4; break;
        }

        h /= 6;
    }

    return h;
}

function nameOfHue(hue) {

    //colors come from http://www.workwithcolor.com/red-color-hue-range-01.htm
    switch (true) {
        case ((hue >= 0 && hue <= 10) || (hue > 355 && hue <= 360)):
            return "Red";
        case (hue > 10 && hue <= 20):
            return "Red / Orange";
        case (hue > 20 && hue <= 40):
            return "Orange / Brown";
        case (hue > 40 && hue <= 50):
            return "Orange / Yellow";
        case (hue > 50 && hue <= 60):
            return "Yellow";
        case (hue > 60 && hue <= 80):
            return "Yellow / Green";
        case (hue > 80 && hue <= 140):
            return "Green";
        case (hue > 140 && hue <= 169):
            return "Green / Cyan";
        case (hue > 169 && hue <= 200):
            return "Cyan";
        case (hue > 200 && hue <= 220):
            return "Cyan / Blue";
        case (hue > 220 && hue <= 240):
            return "Blue";
        case (hue > 240 && hue <= 280):
            return "Blue / Magenta";
        case (hue > 280 && hue <= 320):
            return "Magenta";
        case (hue > 320 && hue <= 330):
            return "Magenta / Pink";
        case (hue > 330 && hue <= 345):
            return "Pink";
        case (hue > 345 && hue <= 355):
            return "Pink / Red";
        default:
            return "Grey";
    }

}

function getColorName(hex) {

    //if (colorNameInput.val() !== '') {
    //    selectedPaintColor = colorNameInput.val();
    //    return selectedPaintColor;
    //}
    //else {
    //    hex = '#ffffff';
    //}
    if (hex.trim().length === 0) { hex = '#ffffff'; }
    var rgb = hexToRgb(hex);
    var hue = getHue(rgb.r, rgb.g, rgb.b)
    return 'Custom ' + nameOfHue(hue * 360) + ' (' + hex + ')';
}



function setRGBVals(hexColor) {
    if (hexColor === '') {
        hexColor = '#ffffff';
    }
    var rbg = hexToRgb(hexColor);
    colorR.val(rbg.r).change();
    colorG.val(rbg.g).change();
    colorB.val(rbg.b).change();
    colorRsm.val(rbg.r).change();
    colorGsm.val(rbg.g).change();
    colorBsm.val(rbg.b).change();
}

function componentToHex(c) {
    var hex = c.toString(16);
    return hex.length === 1 ? "0" + hex : hex;
}

function rgbToHex(r, g, b) {
    return componentToHex(r) + componentToHex(g) + componentToHex(b);
}

function setHex(control) {
    if ($(control).val().length === 6) {
        var newColor = $(control).val().replace('#', '');
        //f.setColor('#' + newColor);
        //f2.setColor('#' + newColor);
        setRGBVals(newColor);
    }
    else if ($(control).val().replace('#', '').length > 6) {
        alert('Hex values are only 6 characters long');
        $(control).val($(control).val().substr(0, 6));

    }

}
$('#colorNumber').bind('input', function (e) {
    setHex(this);
});
//$('#colorNumber-sm').bind('input', function (e) {
//    setHex(this);
//});

function setRGB(control, small) {
    if ($(control).val().length === 0) {
        return;
    }
    if ($(control).val() < 0) {
        alert('RGB values must be between 0 and 255');
        $(control).val(0);
    }
    else if ($(control).val() > 255) {
        alert('RGB values must be between 0 and 255');
        $(this).val(255);
    }
    var hex = "";
    if (small) {
        hex = rgbToHex(parseInt(colorRsm.val()), parseInt(colorGsm.val()), parseInt(colorBsm.val()));
    }
    else {
        hex = rgbToHex(parseInt(colorR.val()), parseInt(colorG.val()), parseInt(colorB.val()));
    }
    //colorInput.val(hex);
    colorInput.chromoselector('setColor', '#' + hex);
    //colorInput2.val(hex);
    //colorInput2.chromoselector('setColor', '#' + hex);
    //f.setColor('#' + hex);
    //f2.setColor('#' + hex);
}
$('.rgbInput input').bind('input', function () {
    setRGB(this, false);
});
//$('.rgbInput-sm input').bind('input', function () {
//    setRGB(this, true);
//});
function LightenDarkenColor(col, amt) {

    var usePound = false;

    if (col[0] === "#") {
        col = col.slice(1);
        usePound = true;
    }

    var num = parseInt(col, 16);

    var r = (num >> 16) + amt;

    if (r > 255) r = 255;
    else if (r < 0) r = 0;

    var b = ((num >> 8) & 0x00FF) + amt;

    if (b > 255) b = 255;
    else if (b < 0) b = 0;

    var g = (num & 0x0000FF) + amt;

    if (g > 255) g = 255;
    else if (g < 0) g = 0;

    return (usePound ? "#" : "") + (g | (b << 8) | (r << 16)).toString(16);

}

$('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
    if (e.target.toString().includes('panel3')) {
        $('#search-container').css('background-color', '#efefef');
        setColor('#ffffff');
    }
});

$(document).on('focus', '#colorName', function () {
    $(this).val('');
});

$(document).on('show.bs.modal', '#ql-modal', function (event) {

    var button = $(event.relatedTarget); // Button that triggered the modal
    var prodID = button.data('prodid');
    var modal = $(this);
    modal.find('.modal-title').text('');
    modal.find('#product-title').css('opacity', '0');
    modal.find('.price-after').css('opacity', '0');
    modal.find('#artist-name').css('opacity', '0');
    modal.find('#description').css('opacity', '0');
    modal.find('#modal-store-logo').css('opacity', '0');
    modal.find('#keywords a').remove();
    modal.find('#main-img').attr('src', '/lib/material-bootstrap/img/lightbox/preloader.gif');
    $.ajax({
        url: '/artprints/productdetail',
        type: 'GET',
        contentType: 'application/json',
        data: { id: prodID },
        success: function (data) {
            modal.find('#product-title').text(data.title).css('opacity', '1').css('transition', '0.5s');
            modal.find('#main-img').fadeOut(function () {
                $(this).attr('src', data.images[0].sourceURLLarge).load(function () { $(this).fadeIn(); });
            });
            modal.find('.price-after').html('base price <b>$' + data.price.toFixed(2) + '</b><sup>*<sup>').css('opacity', '1').css('transition', '0.5s');
            modal.find('#modal-store-logo').attr('src', data.supplier.logo).load(function () { $(this).css('opacity', '1').css('transition', '0.5s'); });
            if (data.artist !== null && data.artist.name !== null && data.artist.name.length > 0) {
                modal.find('#artist-name').show();
                modal.find('#artist-link').attr('href', data.artist.slugURL).html(data.artist.name + ' <small>(view more)</small>');
                modal.find('#artist-name').css('opacity', '1').css('transition', '0.5s');
                if (data.supplier.name === 'Red Bubble' || data.supplier.name === 'Imagekind') {
                    modal.find('.indie-artist').show();
                }
                else {
                    modal.find('.indie-artist').hide();
                }
            }
            else {
                modal.find('#artist-name').hide();
            }

            modal.find('#buy-now').attr('href', data.affiliateURL);

            if (data.description !== null && data.description.length > 0) {
                modal.find('#description').text(data.description).fadeIn();
            }
            else {
                modal.find('#description').hide();
            }


            if (data.keywords !== null && data.keywords.length > 0) {
                $.each(data.keywords.split(','), function () {
                    var link = '<a href="/' + this.trim().replace(' ', '-') + '" class="btn btn-sm primary-rounded-outline waves-effect">' + this.trim() + '</a>';
                    modal.find('#keywords').append(link);
                });
                modal.find('#keywords').append('<hr>')
                modal.find('#keywords').fadeIn();
            }
            else {
                modal.find('#keywords').hide();
            }
            modal.find('#detail-link').attr('href', button.attr('href'));
            modal.find('.btn-fb').attr('data-link', encodeURIComponent(window.location.origin + button.attr('href')));

            var twitterURL = 'https://twitter.com/intent/tweet?text=';
            twitterURL += encodeURIComponent('Check out this nice art print');
            twitterURL += '&url=' + encodeURIComponent(window.location.origin + button.attr('href'));
            twitterURL += '&via=artbyhue';

            modal.find('.btn-tw').attr('href', twitterURL);

            var pinURL = 'https://www.pinterest.com/pin/create/button/?url=';
            pinURL += encodeURIComponent(window.location.origin + button.attr('href'));
            pinURL += '&media=' + encodeURIComponent(data.images[0].sourceURLLarge);
            pinURL += '&description=' + encodeURIComponent('Art Print titled ' + data.title + ' found at Art By Hue');

            modal.find('.btn-pin').attr('href', pinURL);

            var gplusURL = 'https://plus.google.com/share?url=';
            gplusURL += encodeURIComponent(window.location.origin + button.attr('href'));
            //gplusURL += encodeURIComponent('Check out this nice art print');

            modal.find('.btn-gplus').attr('href', gplusURL);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            console.debug(thrownError);
        }
    });
});

$(document).on('click', '.btn-fb', function () {
    FB.ui({
        method: 'share',
        display: 'popup',
        href: document.querySelector("link[rel='canonical']").href
    }, function (response) { });
});

$(function () {
    $(".button-collapse").sideNav();
    $('[data-toggle="popover"]').popover({
        trigger: 'focus'
    });

    $('#paintColorSelect').addClass('animated').addClass('bounceOutRight');

    tempForeColor = getForegroundColor(tempColor);
    propegateColor(false);

    var updatePreview = function () {
        var hexColor = $('#colorNumber').chromoselector("getColor");
        setColor(hexColor.getHexString());
        $('#search-container').css('background-color', hexColor.getHexString());
    };

    $('#colorNumber').chromoselector({
        target: '#pickerDiv',
        autoshow: false,
        preview: false,
        resizable: false,
        roundcorners: false,
        //create: updatePreview,
        update: updatePreview,
        create: function () {
            $(this).chromoselector('show', 0);
        },
        str2color: function (str) {
            colorHiddenInput.val(str);
            return '#' + str;
        },
        color2str: function (color) {
            return color.getHexString().substring(1);
        },

        width: 200
    });

    var setPanelColors = function () {
        var hexColor = $('#ColorHome').chromoselector("getColor").getHexString();
        tempForeColor = getForegroundColor(hexColor);
        $('#home-color-selector').css('background-color', hexColor).css('color', tempForeColor);
        colorNameHome.val(getColorName(hexColor));
        $(this).chromoselector('show', 0);
    };

    $('#ColorHome').chromoselector({
        target: '#pickerDivHome',
        autoshow: false,
        preview: false,
        resizable: false,
        roundcorners: false,
        //create: setPanelColors,
        update: setPanelColors,
        create: function () {
            var hexColor = '#006400'; //$('#ColorHome').chromoselector("getColor").getHexString();
            tempForeColor = getForegroundColor(hexColor);
            $('#home-color-selector').css('background-color', hexColor).css('color', tempForeColor);
            $(this).chromoselector('show', 0);
        },
        str2color: function (str) {
            $('#ColorHome').val(str);
            return '#' + str;
        },
        color2str: function (color) {
            return color.getHexString().substring(1);
        },
        
        width: 200
    });


    var dataSource = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        //remote: {
        //    url: '/lib/autocomplete/pcolors.json?q=%QUERY',
        //    filter: function (data) {
        //        // Map the remote source JSON array to a JavaScript object array
        //        return $.map(data.colors, function (color) {
        //            console.log(color);
        //            return {
        //                name: color.name,
        //                hex: color.hex
        //            };
        //        });
        //    }
        //},
        prefetch: {
            url: '/content/pcolors.json',
            filter: function (data) {
                return $.map(data.colors, function (color) {
                    return {
                        name: color.name,
                        hex: color.hex
                    };
                });
            }
        }
    });

    dataSource.clearPrefetchCache();
    dataSource.initialize();

    colorNameInput.typeahead(null, {
        name: 'name',
        display: 'name',
        limit: 25,
        minLength: 1,
        source: dataSource,
        templates: {
            empty: [
              '<div class="empty-message">',
                'unable to find any matching paints/colors',
              '</div>'
            ].join('\n'),
            suggestion: Handlebars.compile('<div><div class="tt-color-swatch" style="background-color: {{hex}}"></div><div class="tt-color-name">{{name}}</div><div style="clear: both;"></div>')
        }
    }).on('typeahead:selected', function (obj, datum) {
        selectedColor = datum.hex;
        colorInput.chromoselector('setColor', selectedColor);
        //colorInput2.chromoselector('setColor', selectedColor);
        setColor(datum.hex);
        $(colorHiddenInput).val(datum.hex.replace('#', ''));
    });

    var tagsDataSource = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace,
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/suggest/%QUERY',
            wildcard: '%QUERY'
        }

    });

    tagsDataSource.initialize();

    keywordsInput.typeahead(null, {
        name: 'tags',
        async: true,
        limit: 14,
        minLength: 3,
        source: tagsDataSource
    });
});





