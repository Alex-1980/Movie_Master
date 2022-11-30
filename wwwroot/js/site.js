// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let searchBox = document.querySelector('#searchBox');
searchBox.focus();

// YouTube Player Slide-up/Slide-down
let ytPlayer = document.querySelector('#youtubePlayer');

let isOpen = false;
function togglePlayer() {
    if(isOpen) {
        $('#youtubePlayer').animate({
            marginTop: '800px'
        }, 300, 'swing');
        isOpen = false;
    } else {        
        $('#youtubePlayer').animate({
            marginTop: '15px'
        }, 360, 'swing');
        isOpen = true;
    }
}