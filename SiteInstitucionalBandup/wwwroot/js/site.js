// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function slash(name) {
    let elem = document.getElementById(name);
    let label = $('label[for="' + name + '"]')
    if (elem.checked) {
        label.addClass('traced')
    } else {
        label.removeClass('traced')
    }
}

$(document).ready(function () {

    /* 1. Visualizing things on Hover - See next part for action on click */
    $('#stars li').on('mouseover', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently mouse on

        // Now highlight all the stars that's not after the current hovered star
        $(this).parent().children('li.star').each(function (e) {
            if (e < onStar) {
                $(this).addClass('hover');
            }
            else {
                $(this).removeClass('hover');
            }
        });

    }).on('mouseout', function () {
        $(this).parent().children('li.star').each(function (e) {
            $(this).removeClass('hover');
        });
    });


    /* 2. Action to perform on click */
    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently selected
        var stars = $(this).parent().children('li.star');

        for (i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('selected');
        }

        for (i = 0; i < onStar; i++) {
            $(stars[i]).addClass('selected');
        }

        // JUST RESPONSE (Not needed)
        var ratingValue = parseInt($('#stars li.selected').last().data('value'), 10);
        console.log(ratingValue);
    });
});




// Script slide
document.addEventListener("DOMContentLoaded", function () {
	let next = document.querySelector('.next');
	let prev = document.querySelector('.prev');

	next.addEventListener('click', function () {
		let items = document.querySelectorAll('.item')
		document.querySelector('.slide').appendChild(items[0]);
	});

	prev.addEventListener('click', function () {
		let items = document.querySelectorAll('.item')
		document.querySelector('.slide').prepend(items[items.length - 1]);
	});
});

/* Script menu full
document.addEventListener("DOMContentLoaded", function () {
	document.querySelector('.menu-bar').addEventListener('click', function (e) {
		e.preventDefault();
		document.querySelector('#menuaberto').classList.toggle('close');
	});
}) */

// remove dragstart
document.querySelectorAll('a, img').forEach(drag => {
	drag.addEventListener('dragstart', function(e){
		e.preventDefault();
	});
});

// show/hidden menu
const showAnim = gsap.from('.header-slide-menu', { 
	yPercent: -100,
	paused: true,
	duration: 0.2
  }).progress(1);
  
  ScrollTrigger.create({
	onUpdate: (self) => {
	  self.direction === -1 ? showAnim.play() : showAnim.reverse()
	}
  });

  document.querySelector('.mouse-roll').addEventListener('click', function() {
	document.querySelector('.banner-content').scrollIntoView({
		block: 'start',
		behavior: 'smooth'
	});
  });

// slideshow_home

var swiper = new Swiper(".mySwiper", {
	centeredSlides: true,
	autoplay: {
	  delay: 2500,
	  disableOnInteraction: false,
	},
	effect: 'fade',
  		fadeEffect: {
    	crossFade: true
  },
	pagination: {
	  el: ".swiper-pagination",
	  clickable: true,
	},
  });
