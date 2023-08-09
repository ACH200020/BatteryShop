
document.addEventListener('DOMContentLoaded', function () {

   var thumbnails1 = new Splide('#special__offer__splide__list1', {
      direction: 'ttb',
      height: '100%',
      autoHeight: true,
      rewind: true,
      pagination: false,
      isNavigation: true,
      arrows: false,
      cover: true,
      wheel: true,
   });

   var main1 = new Splide('#special__offer__splide__contect1', {
      type: 'fade',
      rewind: true,
      pagination: false,
      arrows: false,
   });

   main1.sync(thumbnails1);
   main1.mount();
   thumbnails1.mount();


   var thumbnails2 = new Splide('#special__offer__splide__list2', {
      direction: 'ttb',
      height: '100%',
      autoHeight: true,
      rewind: true,
      pagination: false,
      isNavigation: true,
      arrows: false,
      cover: true,
      wheel: true,
   });

   var main2 = new Splide('#special__offer__splide__contect2', {
      type: 'fade',
      rewind: true,
      pagination: false,
      arrows: false,
   });

   main2.sync(thumbnails2);
   main2.mount();
   thumbnails2.mount();

});



$('.owl-first-carousel').owlCarousel({
   rtl: true,
   loop: true,
   margin: 10,
   autoplay: true,
   autoplayTimeout: 2000,
   autoplayHoverPause: true,
   responsive: {
      0: {
         items: 1
      },
      515: {
         items: 2,
         nav: true,
      },
      768: {
         items: 3,
         nav: true,
      },
      992: {
         items: 4,
         nav: true,
      },
      1200: {
         items: 5,
         nav: true,
      }
   }
});
$('.owl-second-carousel').owlCarousel({
   rtl: true,
   loop: true,
   margin: 10,
   autoplay: true,
   autoplayTimeout: 2000,
   autoplayHoverPause: true,
   responsive: {
      0: {
         items: 1
      },
      768: {
         items: 1,
      },
      992: {
         items: 2,
         nav: true,
      },
      1200: {
         items: 3,
         nav: true,
      }
   }
});
$('.owl-four-carousel').owlCarousel({
   rtl: true,
   loop: true,
   margin: 10,
   autoplay: true,
   autoplayTimeout: 2000,
   autoplayHoverPause: true,
   responsive: {
      0: {
         items: 1
      },
      768: {
         items: 2,
         nav: true,
      },
      992: {
         items: 3,
         nav: true,
      },
      1200: {
         items: 4,
         nav: true,
      }
   }
});
$('.owl-five-carousel').owlCarousel({
   rtl: true,
   loop: true,
   margin: 10,
   autoplay: true,
   autoplayTimeout: 2000,
   autoplayHoverPause: true,
   responsive: {
      0: {
         items: 1
      },
      768: {
         items: 2,
         nav: true,
      },
      992: {
         items: 3,
         nav: true,
      },
      1200: {
         items: 4,
         nav: true,
      }
   }
});
$('.owl-six-carousel').owlCarousel({
   rtl: true,
   loop: true,
   margin: 20,
   autoplay: true,
   autoplayTimeout: 2000,
   autoplayHoverPause: true,
   responsive: {
      0: {
         items: 1
      },
      768: {
         items: 2,
         nav: true,
      },
      992: {
         items: 3,
         nav: true,
      },
      1200: {
         items: 4,
         nav: true,
      }
   }
});
$('.owl-seven-carousel').owlCarousel({
   rtl: true,
   loop: true,
   margin: 20,
   autoplay: true,
   autoplayTimeout: 2000,
   autoplayHoverPause: true,
   responsive: {
      0: {
         items: 1
      },
      768: {
         items: 2,
         nav: true,
      },
      992: {
         items: 2,
         nav: true,
      },
      1200: {
         items: 2,
         nav: true,
      }
   }
});



$(document).ready(function () {

   var sync1 = $("#sync1");
   var sync2 = $("#sync2");
   var slidesPerPage = 4; //globaly define number of elements per page
   var syncedSecondary = true;

   sync1.owlCarousel({
      items: 1,
      rtl: true,
      slideSpeed: 2000,
      nav: false,
      autoplay: false,
      loop: true,
      responsiveRefreshRate: 200,
      navText: ['<svg width="100%" height="100%" viewBox="0 0 11 20"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M9.554,1.001l-8.607,8.607l8.607,8.606"/></svg>', '<svg width="100%" height="100%" viewBox="0 0 11 20" version="1.1"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M1.054,18.214l8.606,-8.606l-8.606,-8.607"/></svg>'],
   }).on('changed.owl.carousel', syncPosition);

   sync2
      .on('initialized.owl.carousel', function () {
         sync2.find(".owl-item").eq(0).addClass("current");
      })
      .owlCarousel({
         items: slidesPerPage,
         rtl: true,
         nav: true,
         smartSpeed: 200,
         slideSpeed: 500,
         slideBy: slidesPerPage, //alternatively you can slide by 1, this way the active slide will stick to the first item in the second carousel
         responsiveRefreshRate: 100
      }).on('changed.owl.carousel', syncPosition2);

   function syncPosition(el) {
      //if you set loop to false, you have to restore this next line
      //var current = el.item.index;

      //if you disable loop you have to comment this block
      var count = el.item.count - 1;
      var current = Math.round(el.item.index - (el.item.count / 2) - .5);

      if (current < 0) {
         current = count;
      }
      if (current > count) {
         current = 0;
      }

      //end block

      sync2
         .find(".owl-item")
         .removeClass("current")
         .eq(current)
         .addClass("current");
      var onscreen = sync2.find('.owl-item.active').length - 1;
      var start = sync2.find('.owl-item.active').first().index();
      var end = sync2.find('.owl-item.active').last().index();

      if (current > end) {
         sync2.data('owl.carousel').to(current, 100, true);
      }
      if (current < start) {
         sync2.data('owl.carousel').to(current - onscreen, 100, true);
      }
   }

   function syncPosition2(el) {
      if (syncedSecondary) {
         var number = el.item.index;
         sync1.data('owl.carousel').to(number, 100, true);
      }
   }

   sync2.on("click", ".owl-item", function (e) {
      e.preventDefault();
      var number = $(this).index();
      sync1.data('owl.carousel').to(number, 300, true);
   });
});



$(document).ready(function () {

   var sync11 = $(".sync11");
   var sync12 = $(".sync12");
   var slidesPerPage = 4; //globaly define number of elements per page
   var syncedSecondary = true;

   sync11.owlCarousel({
      items: 1,
      rtl: true,
      slideSpeed: 2000,
      nav: false,
      autoplay: false,
      loop: true,
      responsiveRefreshRate: 200,
      navText: ['<svg width="100%" height="100%" viewBox="0 0 11 20"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M9.554,1.001l-8.607,8.607l8.607,8.606"/></svg>', '<svg width="100%" height="100%" viewBox="0 0 11 20" version="1.1"><path style="fill:none;stroke-width: 1px;stroke: #000;" d="M1.054,18.214l8.606,-8.606l-8.606,-8.607"/></svg>'],
   }).on('changed.owl.carousel', syncPosition);

   sync12
      .on('initialized.owl.carousel', function () {
         sync12.find(".owl-item").eq(0).addClass("current");
      })
      .owlCarousel({
         items: slidesPerPage,
         rtl: true,
         nav: true,
         smartSpeed: 200,
         slideSpeed: 500,
         slideBy: slidesPerPage, //alternatively you can slide by 1, this way the active slide will stick to the first item in the second carousel
         responsiveRefreshRate: 100
      }).on('changed.owl.carousel', syncPosition2);

   function syncPosition(el) {
      //if you set loop to false, you have to restore this next line
      //var current = el.item.index;

      //if you disable loop you have to comment this block
      var count = el.item.count - 1;
      var current = Math.round(el.item.index - (el.item.count / 2) - .5);

      if (current < 0) {
         current = count;
      }
      if (current > count) {
         current = 0;
      }

      //end block

      sync12
         .find(".owl-item")
         .removeClass("current")
         .eq(current)
         .addClass("current");
      var onscreen = sync12.find('.owl-item.active').length - 1;
      var start = sync12.find('.owl-item.active').first().index();
      var end = sync12.find('.owl-item.active').last().index();

      if (current > end) {
         sync12.data('owl.carousel').to(current, 100, true);
      }
      if (current < start) {
         sync12.data('owl.carousel').to(current - onscreen, 100, true);
      }
   }

   function syncPosition2(el) {
      if (syncedSecondary) {
         var number = el.item.index;
         sync11.data('owl.carousel').to(number, 100, true);
      }
   }

   sync12.on("click", ".owl-item", function (e) {
      e.preventDefault();
      var number = $(this).index();
      sync11.data('owl.carousel').to(number, 300, true);
   });
});




$(document).scroll(function () {
   var y = $(this).scrollTop();
   if (y > 800) {
      $('.mobile__nav__bottom').fadeIn();
   } else {
      $('.mobile__nav__bottom').fadeOut();
   }
});



$(document).scroll(function () {
   var y = $(this).scrollTop();
   if (y > 1200) {
      $('.footer__options').fadeIn();
   } else {
      $('.footer__options').fadeOut();
   }
});



(function () {

   window.inputNumber = function (el) {

      var min = el.attr('min') || false;
      var max = el.attr('max') || false;

      var els = {};

      els.dec = el.prev();
      els.inc = el.next();

      el.each(function () {
         init($(this));
      });

      function init(el) {

         els.dec.on('click', decrement);
         els.inc.on('click', increment);

         function decrement() {
            var value = el[0].value;
            value--;
            if (!min || value >= min) {
               el[0].value = value;
            }
         }

         function increment() {
            var value = el[0].value;
            value++;
            if (!max || value <= max) {
               el[0].value = value++;
            }
         }
      }
   }
})();

inputNumber($('.input-number'));



function openCity(evt, megaTab) {
   // Declare all variables
   var i, tabcontent, tablinks;

   // Get all elements with class="tabcontent" and hide them
   tabcontent = document.getElementsByClassName("tabcontent");
   for (i = 0; i < tabcontent.length; i++) {
      tabcontent[i].style.display = "none";
   }

   // Get all elements with class="tablinks" and remove the class "active"
   tablinks = document.getElementsByClassName("tablinks");
   for (i = 0; i < tablinks.length; i++) {
      tablinks[i].className = tablinks[i].className.replace(" active", "");
   }

   // Show the current tab, and add an "active" class to the link that opened the tab
   document.getElementById(megaTab).style.display = "block";
   evt.currentTarget.className += " active";
};



(function ($) {
   $(window).on("load", function () {
      $(".product__checkbox").mCustomScrollbar();

      $("#slider-range").slider({
         range: true,
         min: 130,
         max: 500,
         values: [130, 250],
         slide: function (event, ui) {
            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
         }
      });
      $("#amount").val("$" + $("#slider-range").slider("values", 0) +
         " - $" + $("#slider-range").slider("values", 1));


      $("#slider-range-mobile").slider({
         range: true,
         min: 130,
         max: 500,
         values: [130, 250],
         slide: function (event, ui) {
            $("#amount-mobile").val("$" + ui.values[0] + " - $" + ui.values[1]);
         }
      });
      $("#amount-mobile").val("$" + $("#slider-range-mobile").slider("values", 0) +
         " - $" + $("#slider-range-mobile").slider("values", 1));
   });
})(jQuery);






// Set the date we're counting down to
var countDownDate = new Date("May 15, 2022 12:00:00").getTime();

// Update the count down every 1 second
var x = setInterval(function () {

   // Get today's date and time
   var now = new Date().getTime();

   // Find the distance between now and the count down date
   var distance = countDownDate - now;

   // Time calculations for days, hours, minutes and seconds
   var days = Math.floor(distance / (1000 * 60 * 60 * 24));
   var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
   var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
   var seconds = Math.floor((distance % (1000 * 60)) / 1000);

   // Output the result in an element with id="demo"
   var finalDate = seconds + " : " + minutes + " : " + hours + " : " + days;

   $('.item .card-timer').html(finalDate);
   $('.item .card-timer__unbg').html(finalDate);
   $('.card-img-top-bg .card-timer').html(finalDate);
   $('.product__count__down').html(finalDate);
   $('.poduct__timmer span').html(finalDate);

   // If the count down is over, write some text 
   if (distance < 0) {
      clearInterval(x);
      $('.item .card-timer').html("بدون تخفیف");
   }
}, 1000);




$('#product__nav__more__btn').click(function (e) {
   e.preventDefault();
   $('.product__nav__more').slideToggle();
});



$('.similar__product__slider').owlCarousel({
   rtl: true,
   loop: true,
   margin: 10,
   autoplay: true,
   autoplayTimeout: 2000,
   autoplayHoverPause: true,
   responsive: {
      0: {
         items: 1
      },
      576: {
         items: 2,
         nav: true,
      },
      992: {
         items: 3,
         nav: true,
      },
      1200: {
         items: 4,
         nav: true,
      }
   }
});


$(".mCustomScrollbar").mCustomScrollbar({
   axis: "x", // horizontal scrollbar
   theme: "dark-thin",
});


$(".points__input__weak").focus(function (e) {
   $("button.product__comment__points__weak").fadeIn();
});

$('button.product__comment__points__weak').click(function (e) {
   var taskValue = $(".points__input__weak").val();

   $('<div  class="points__input__values weak__points__values px-2 py-1 align-items-center d-flex mb-2">' + taskValue + '<a class="btn p-0 fw-bold float-end ms-auto" href="#" role="button">x</a></div>').appendTo('.points__input__value__weak');
});


$(".points__input__strength").focus(function (e) {
   $("button.product__comment__points__strength").fadeIn();
});

$('button.product__comment__points__strength').click(function (e) {
   var taskValue = $(".points__input__strength").val();

   $('<div class="points__input__values strength__points__values px-2 py-1 align-items-center d-flex mb-2">' + taskValue + ' <a class="btn p-0 fw-bold float-end ms-auto" href="#" role="button">x</a></div>').appendTo('.points__input__value__strength');
});