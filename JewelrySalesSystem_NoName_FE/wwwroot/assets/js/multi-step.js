function () {
    let currentTab = 0;
    showTab(currentTab);

    function showTab(n) {
        let x = document.getElementsByClassName("step");
        x[n].classList.add("active");
        if (n === 0) {
            document.getElementById("prevBtn").style.display = "none";
        } else {
            document.getElementById("prevBtn").style.display = "inline";
        }
        if (n === (x.length - 1)) {
            document.getElementById("nextBtn").innerHTML = "Submit";
        } else {
            document.getElementById("nextBtn").innerHTML = "Next";
        }
        updateStepIndicator(n);
    }

    window.nextPrev = function (n) {
        let x = document.getElementsByClassName("step");
        if (n == 1 && !validateForm()) return false;
        x[currentTab].classList.remove("active");
        currentTab += n;
        if (currentTab >= x.length) {
            document.getElementById("regForm").submit();
            return false;
        }
        showTab(currentTab);
    };

    function validateForm() {
        let valid = true;
        let x = document.getElementsByClassName("step");
        let y = x[currentTab].getElementsByTagName("input");
        for (var i = 0; i < y.length; i++) {
            if (y[i].value == "") {
                y[i].classList.add("is-invalid");
                valid = false;
            } else {
                y[i].classList.remove("is-invalid");
            }
        }
        return valid;
    }

    function updateStepIndicator(n) {
        let steps = document.getElementsByClassName("stepwizard-step");
        for (let i = 0; i < steps.length; i++) {
            steps[i].getElementsByTagName("a")[0].classList.remove("btn-primary");
            steps[i].getElementsByTagName("a")[0].classList.add("btn-default");
            steps[i].getElementsByTagName("a")[0].disabled = true;
        }
        steps[n].getElementsByTagName("a")[0].classList.add("btn-primary");
        steps[n].getElementsByTagName("a")[0].disabled = false;
    }
    function toggleExpandable() {
        var content = document.getElementById("expandableContent");
        content.classList.toggle("expanded");
    }
}) ();