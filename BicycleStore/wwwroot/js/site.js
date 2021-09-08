// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#car-list')
    .load('/Home/BicyclesList')

$('#search').on('click', function () {
    let searchText = $('#searchText').val()

    $('#car-list')
        .load(`/Home/BicyclesList?search=${searchText}`)
})

//$('#sortbyman').on('click', function () {
//    $('#car-list')
//        .load(`/Home/BicyclesList?sortType=ManufacturerAsc`)
//})

//$('#sortbymodel').on('click', function () {
//    $('#car-list')
//        .load(`/Home/BicyclesList?sortType=ModelAsc`)
//})
//$('#sortbyprice').on('click', function () {
//    $('#car-list')
//        .load(`/Home/BicyclesList?sortType=PriceAsc`)
//})
//$('#sortbyweight').on('click', function () {
//    $('#car-list')
//        .load(`/Home/BicyclesList?sortType=WeightAsc`)
//})
//$('#sortbyweels').on('click', function () {
//    $('#car-list')
//        .load(`/Home/BicyclesList?sortType=WeelsRadiusAsc`)
//})
//$('#sortbybrakes').on('click', function () {
//    $('#car-list')
//        .load(`/Home/BicyclesList?sortType=BrakesAsc`)
//})
//$('#sortbytype').on('click', function () {
//    $('#car-list')
//        .load(`/Home/BicyclesList?sortType=TypeAsc`)
//})

$('#back').on('click', function () {
    var searchText = document.getElementById('searchText')
    var search = searchText.innerHTML
    var current_pagenumber = document.getElementById('pagenumber')
    var page_prev = current_pagenumber.innerHTML
    var page_now = parseInt(page_prev) - 1;
    if (page_now <= 0) {
        $('#car-list')
            .load(`/Home/BicyclesList/?page=${1}`)
    }
    else {
        $('#car-list')
            .load(`/Home/BicyclesList/?page=${page_now}`)
    }
})

$('#forward').on('click', function () {
    var searchText = document.getElementById('searchText')
    var search = searchText.innerHTML
    var current_pagenumber = document.getElementById('pagenumber')
    var page_prev = current_pagenumber.innerHTML
    var page_now = parseInt(page_prev) + 1;
    var totalpages = document.getElementById('totalpages')
    var pages = totalpages.innerHTML
    if (parseInt(page_now) > parseInt(pages)) {
        $('#car-list')
            .load(`/Home/BicyclesList/?page=${pages}`)
    }
    else {
        $('#car-list')
            .load(`/Home/BicyclesList/?page=${page_now}`)
    }
})