
$('.my-carousel').carousel({
  interval:4000
});
/*account collapse-BEGIN*/
$('#collapseMyCompany').on('show.bs.collapse', function () {
    $('#collapseBonuses').collapse('hide')
    $('#collapseProgress').collapse('hide')
});


$('#collapseBonuses').on('show.bs.collapse', function () {
    $('#collapseMyCompany').collapse('hide')
    $('#collapseProgress').collapse('hide')
});
$('#collapseProgress').on('show.bs.collapse', function () {
    $('#collapseMyCompany').collapse('hide')
    $('#collapseBonuses').collapse('hide')
});
/*account collapse-END*/
function registration() {
  var email=document.getElementById('email').value;
  var name


}


function setEqualHeight() {
    var width = $('body').innerWidth()
    if (width < 1000) {
        return "1"
    }

    console.log("Hello world");
};

function DonnateWithBonus(bonusId,sum){
    document.getElementById('idBonnus').value = bonusId
    document.getElementById('Sum').value = sum
    document.getElementById('submitDonnate').click()
}

$(window).resize(setEqualHeight);

$(document).ready(function () {
    $(window).scroll(function () {
        var header_height = $('header').height() + $('.navbar').height();
        if ($(this).scrollTop() > header_height) {
            $('.navbar').stop(true, true).addClass('fixed-top').addClass('animated slideInDown');
        } else {
            $('.navbar').stop().removeClass('fixed-top');
        }
    });
});
 

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            document.getElementById('EditImg').style.backgroundImage = "url(" + e.target.result + ")";
            $('#EditImg').attr('src', $('#StreamImg').val());
            $('#buttonAvatarSave').attr('type', 'submit');
            $('#buttonAvatarCancell').attr('type', 'button');

        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#StreamImg").change(function () {
    readURL(this);
});

function setYouTubeUrl() {
    if ($('#StreamVideo').val()) {

        $('#EditVideo').attr('src', $('#StreamVideo').val());
    }
};
$("#btnAvatar").click(function() {
    document.getElementById("StreamImg").click();
});
function CloseShow() {
    $('#Add').attr('type', 'hidden');
    $('#Cancel').attr('type', 'hidden');
    $("#Show").attr('type', 'button');
    $('#Link').attr('type', 'hidden');
};

function Show() {
    $('#Add').attr('type', 'submit');
    $('#Cancel').attr('type', 'button');
    $("#Show").attr('type', 'hidden');
    $('#Link').attr('type', 'text');
};


