var JobCoin = function () {

    var dropDownNationality;    

    var WireUpUIEvents = function () {

        $("#btn-submit").click(function () {
            var entry = $("#form_addresses").val();
            if(entry.length > 0){
                var addresses = entry.split("\n")
                Submit();
            }
            else{
                console.log("empty values");
            }            
        });
    };
    
    var Submit = function(){
        $.ajax({
            url: "http://rest-service.guides.spring.io/greeting"
        }).then(function(data) {
           console.log(data.id);
           console.log(data.content);
        });
    };

   

   

    var Init = function () {
        WireUpUIEvents();
    }();



}();


