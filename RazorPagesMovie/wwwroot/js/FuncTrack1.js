document.addEventListener("DOMContentLoaded", function() {
    
    var toggleButton = document.getElementById("toggleButton");
    var elementsToToggle = document.querySelectorAll(".card, .table");

    
    toggleButton.addEventListener("click", function() {
        
        elementsToToggle.forEach(function(element) {
            
            element.classList.toggle("d-none");
        });
    });

    var toggleButton2 = document.getElementById('calculateButton');
    var hiddenForm = document.getElementById('hiddenForm');

    toggleButton2.addEventListener('click', function() {
        if (hiddenForm.style.display === 'none') {
            hiddenForm.style.display = 'block';
        } else {
            hiddenForm.style.display = 'none';
        }
    });

    
});
