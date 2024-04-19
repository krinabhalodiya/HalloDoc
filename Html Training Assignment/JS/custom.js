function sidebar() {
   
    var temp=document.getElementById('sidebar');

    var dissewtting=temp.style.display;

    if(dissewtting==='none')
    {
        
        temp.style.display='block';
   
        document.getElementById('krina').style.width="85vw";
    }
    else
    {
        
        temp.style.display='none';
        document.getElementById('krina').style.width="100vw";
    }
     
}
    
    
$("#files").change(function () {
    console.log("function");
    filename = this.files[0].name;
    $("#choosenfile").text(filename);
});


function openCity(evt, cityName) {
    evt.preventDefault();
    var i, tabcontent, tablinks;
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }
    document.getElementById(cityName).style.display = "block";
    evt.currentTarget.className += " active";
}
