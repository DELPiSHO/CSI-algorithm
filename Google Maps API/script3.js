// Load the Visualization API and the columnchart package.
google.load('visualization', '1', {packages: ['columnchart']});

function initMap() {
  // The following path marks a path from Mt. Whitney, the highest point in the
  // continental United States to Badwater, Death Valley, the lowest point.
  var path = [
      {lat: 29.24, lng: 88.89}, // Hariyon
      {lat: 27.08, lng: 85.56}  // Xigaze
      // przez Himalaje(Mt Everest)
      ];

  var elevator = new google.maps.ElevationService();

  displayPathElevation(path, elevator);
}

function displayPathElevation(path, elevator) {

  elevator.getElevationAlongPath({
    'path': path,
    'samples': 256
  }, plotElevation);
}

function download(elevations) {

		var headers = ['lat', 'lng', 'elevation']
    
    var  heights = [];
    var latlng = [];
    var lng = [];
    
    var length = 0;
    
		let csvContent = "data:text/csv;charset=utf-8,"; 
    
		for (var elevationObject of elevations) {
  		heights.push(elevationObject.elevation);
      latlng.push(elevationObject.location);
      length += 1;
    }
    
    var data = [latlng, heights]
    
    
		let row = headers.join(",");         
    csvContent += row + "\r\n";
        
    for (i = 0; i < length; i++) {
          csvContent += latlng[i]  + ',' + heights[i] + '\r\n';
    }

    var encodedUri = encodeURI(csvContent);
    var link = document.createElement("a");
    link.setAttribute("href", encodedUri);
    link.setAttribute("download", "xigaze-hariyon.csv");
    document.body.appendChild(link);

    link.click();
}

function plotElevation(elevations, status) {

	var trasa1 = document.getElementById('dane_trzecia_trasa');

  download(elevations);

}