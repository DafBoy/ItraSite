$('.my-carousel').carousel({
  interval:4000
});
/*account collapse-BEGIN*/
$('#collapseMyCompany').on('show.bs.collapse', function () {
    $('#collapseInvestmentСompany').collapse('hide')
    $('#collapseProgress').collapse('hide')
});


$('#collapseInvestmentСompany').on('show.bs.collapse', function () {
    $('#collapseMyCompany').collapse('hide')
    $('#collapseProgress').collapse('hide')
});
$('#collapseProgress').on('show.bs.collapse', function () {
    $('#collapseMyCompany').collapse('hide')
    $('#collapseInvestmentСompany').collapse('hide')
});
/*account collapse-END*/
function registration() {
  var email=document.getElementById('email').value;
  var name


}
