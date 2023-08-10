// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    var currentIndex = 0;
    var images = [];
    // Handle navbar button click to toggle content
    $("#navbarToggleButton").click(function (event) {
        event.preventDefault(); // Prevent the default link behavior
        $("#toggleButton").click();
    });

    // Handle content button click to toggle hidden content
    $("#toggleButton").click(function () {
        console.log("Toggle button clicked");
        $("#hiddenContent").toggleClass("visible-content");
    });
    

    // Handle fading images
    var currentIndex = 0;
    var images = [];

    function loadImages() {
        $(".fade-image").each(function () {
            images.push($(this).attr("src"));
        });
    }

    function showImage(index) {
        $(".fade-image").attr("src", images[index]);
    }

    function updateImage(newIndex) {
        if (newIndex >= 0 && newIndex < images.length) {
            showImage(newIndex);
            currentIndex = newIndex;
        }
    }

    loadImages();
    showImage(currentIndex);

    // Handle previous and next buttons using AJAX
    function loadNextImage(step) {
        currentIndex = (currentIndex + step + images.length) % images.length;
        var imageUrl = images[currentIndex];
        $(".fade-image").fadeOut(500, function () {
            $(this).attr("src", imageUrl).fadeIn(500);
        });
    }
    
    
$("#prevButton").click(function (event) {
    event.preventDefault();
    loadNextImage(-1); // Load the previous image
});

$("#nextButton").click(function (event) {
    event.preventDefault();
    loadNextImage(+1); // Load the next image
});


    // Handle form submission using AJAX
    $("#quizForm").submit(function (event) {
        event.preventDefault();
        // Handle form submission logic here using AJAX
    });

});

