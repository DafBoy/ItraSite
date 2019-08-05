var update;
var block = document.getElementById('CommentsBlock')
//Функция для обнавления коментариев
function UpdateComments() {

    var block = document.getElementById('CommentsBlock')
    $.ajax({
        url: '/Company/_Comments?idCompany=' + document.getElementsByName('idCompany')[0].value,
            type: 'POST',
            contentType: "application/json",
            success: function (comments) {
                    block.innerHTML=comments
            }
        });
};


function SendComment() {
    if (document.getElementsByTagName('textarea')[0].value != "") {
        var comment = {
            idCompany: document.getElementsByName('idCompany')[0].value,
            userName: document.getElementsByName('userName')[0].value,
            userComment: document.getElementsByTagName('textarea')[0].value
        }
        $.ajax({
            type: 'POST',
            url: '/Company/_SendComment?idCompany=' + comment.idCompany + "&userName=" + comment.userName + "&userComment=" + comment.userComment,
            contentType: 'application/json; charset=utf-8',
            success: true
        });
        setTimeout(UpdateComments, 500)
        document.getElementsByTagName('textarea')[0].value = ""
    }
};

function autoUpdateComments() {
    update = true;
    setTimeout(function run() {
        func(i);
        if (update) setTimeout(run, 100);
    }, 100);

};

function setLike(idComment,stateLike) {
    console.log(idComment)

    $.ajax({
        url: 'Company/SetLike?idComment=' + idComment + '&userName=' + document.getElementsByName('userName')[0].value+'&stateLike='+stateLike,
        type: 'POST',
        contentType: "application/json",
        success: true
    });
    setTimeout(UpdateComments, 500)
};

function ShowAbout() {
    update = false
    $.ajax({
        url: '/Company/_AboutCompany?idCompany=' + document.getElementsByName('idCompany')[0].value,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: function (about) {
            block.innerHTML = about
        }
    });
}
function ShowBonuses() {
    $.ajax({
        url: '/Company/_Bonuses?idCompany=' + document.getElementsByName('idCompany')[0].value,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: function (about) {
            block.innerHTML = about
        }
    });
}

function ShowGallery() {
    update = false
    $.ajax({
        url: '/Company/_Gallery?idCompany=' + document.getElementsByName('idCompany')[0].value,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: function (gallery) {
            block.innerHTML = gallery

            $(document).ready(function () {  //Для fancy
                $(".fancybox").fancybox({
                    openEffect: "none",
                    closeEffect: "none"
                });

                $(".zoom").hover(function () {

                    $(this).addClass('transition');
                }, function () {

                    $(this).removeClass('transition');
                });
            });
        }
    });
}

function AddImage() {
    console.log(document.getElementById('LoadedFile').files)
    $.ajax({
        url: '/Company/AddImage?idCompany=' + document.getElementsByName('idCompany')[0].value + '&LoadedFile=' + document.getElementById('LoadedFile').files,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: setTimeout(ShowGallery(), 500) 
        
    });
};

function DellImage(idImg) {
    $.ajax({
        url:'/Company/DellImage?idImage='+idImg,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        success: setTimeout(ShowGallery(), 5000) 
    });
}


document.addEventListener("DOMContentLoaded", ShowAbout);






