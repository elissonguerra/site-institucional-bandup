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