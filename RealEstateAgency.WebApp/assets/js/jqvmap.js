$(function(e){
  'use strict'

	//world map
	if ($('#world-map-gdp').length ){

		$('#world-map-gdp').vectorMap({
			map: 'world_en',
			backgroundColor: null,
			color: '#ffffff',
			hoverOpacity: 0.7,
			selectedColor: '#ed5151',
			enableZoom: true,
			showTooltip: true,
			values: sample_data,
			scaleColors: ['#ed5151', '#9c31df'],
			normalizeFunction: 'polynomial'
		});

	}

	//us map
	if ($('#usa_map').length ){

		$('#usa_map').vectorMap({
			map: 'usa_en',
			backgroundColor: null,
			color: '#ffffff',
			hoverOpacity: 0.7,
			selectedColor: '#ed5151',
			enableZoom: true,
			showTooltip: true,
			values: sample_data,
			scaleColors: ['#ed5151', '#9c31df'],
			normalizeFunction: 'polynomial'
		});

	}
	if ($('#german').length ){
		$('#german').vectorMap({
			map : 'germany_en',
			backgroundColor: null,
			color: '#ffffff',
			hoverOpacity: 0.7,
			selectedColor: '#ed5151',
			enableZoom: true,
			showTooltip: true,
			values: sample_data,
			scaleColors: ['#ed5151', '#9c31df'],
			normalizeFunction: 'polynomial'
		});
	}
	if ($('#russia').length ){
		$('#russia').vectorMap({
			map : 'russia_en',
			backgroundColor: null,
			color: '#ffffff',
			hoverOpacity: 0.7,
			selectedColor: '#ed5151',
			enableZoom: true,
			showTooltip: true,
			values: sample_data,
		});
	}

});