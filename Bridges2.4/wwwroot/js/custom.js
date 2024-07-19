// to get current year
function getYear() {
  var currentDate = new Date();
  var currentYear = currentDate.getFullYear();
  document.querySelector("#displayYear").innerHTML = currentYear;
}

getYear();

// client section owl carousel
$(".client_owl-carousel").owlCarousel({
  loop: true,
  margin: 20,
  dots: false,
  nav: true,
  navText: [],
  autoplay: true,
  autoplayHoverPause: true,
  navText: [
    '<i class="fa fa-angle-left" aria-hidden="true"></i>',
    '<i class="fa fa-angle-right" aria-hidden="true"></i>',
  ],
  responsive: {
    0: {
      items: 1,
    },
    600: {
      items: 2,
    },
    1000: {
      items: 2,
    },
  },
});

/** google_map js **/
function myMap() {
  var mapProp = {
    center: new google.maps.LatLng(40.712775, -74.005973),
    zoom: 18,
  };
  var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
}

let pay_section = document.getElementById("print-area");
let pay_btn = document.getElementById("pay-btn");

let payment_section = document.getElementById("payment");
let submit_btn = document.getElementById("submit-btn");

let ticket_section = document.getElementById("ticket");
let ticket_btn = document.getElementById("ticket-btn");

pay_btn.addEventListener("click", function () {
  pay_section.style.display = "none";
  payment_section.style.display = "block";
  ticket_section.style.display = "none";
});

submit_btn.addEventListener("click", function (event) {
  event.preventDefault();
  pay_section.style.display = "none";
  payment_section.style.display = "none";
  ticket_section.style.display = "block";
});
