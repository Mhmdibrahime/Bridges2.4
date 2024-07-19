// add hovered class to selected list item
let list = document.querySelectorAll(".navigation li");

function activeLink() {
  list.forEach((item) => {
    item.classList.remove("hovered");
  });
  this.classList.add("hovered");
}

list.forEach((item) => item.addEventListener("mouseover", activeLink));

// Menu Toggle
let toggle = document.querySelector(".toggle");
let navigation = document.querySelector(".navigation");
let main = document.querySelector(".main");

toggle.onclick = function () {
  navigation.classList.toggle("active");
  main.classList.toggle("active");
};
icon button {
  margin-right: 15px;
  background-color: var(--darkblue-color);
  color: var(--sky-color);
  border: none;
  border-radius: 10px;
  padding: 10px;
  position: relative;
  z-index: 2;
}

.icon button::before {
  content: " ";
  background-image: linear-gradient(
    to right,
    var(--darkblue-color),
    var(--gradient1),
    var(--gradient2),
    var(--sky-color)
  );
  width: 0;
  height: 100%;
  border-radius: 10px;
  position: absolute;
  z-index: -1;
  top: 0;
  left: 0;
  transition: 0.3s;
}

.icon button:hover::before {
  width: 100%;
}

.icon button:hover {
  background-color: transparent;
}
