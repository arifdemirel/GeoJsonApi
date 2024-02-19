import React, { useEffect, useRef, useState } from 'react';
import 'ol/ol.css';
import Map from 'ol/Map';
import View from 'ol/View';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import Draw from 'ol/interaction/Draw';
import VectorLayer from 'ol/layer/Vector';
import VectorSource from 'ol/source/Vector';
import Overlay from 'ol/Overlay';
import { Circle as CircleStyle, Fill, Stroke, Style } from 'ol/style';

const MapComponent = () => {
  const mapRef = useRef(null);
  const popupRef = useRef(null); // Ref for the popup container
  const [activeTool, setActiveTool] = useState(null);
  const [map, setMap] = useState(null);

  useEffect(() => {
    const initialMap = new Map({
      target: mapRef.current,
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
        new VectorLayer({
          source: new VectorSource(),
          style: new Style({
            fill: new Fill({
              color: 'rgba(255, 255, 255, 0.2)',
            }),
            stroke: new Stroke({
              color: '#ffcc33',
              width: 2,
            }),
            image: new CircleStyle({
              radius: 7,
              fill: new Fill({
                color: '#ffcc33',
              }),
            }),
          }),
        }),
      ],
      view: new View({
        center: [0, 0],
        zoom: 2,
      }),
      overlays: [
        new Overlay({
          element: popupRef.current,
          autoPan: true,
          autoPanAnimation: {
            duration: 250,
          },
        }),
      ],
    });
    setMap(initialMap);
  }, []);

  const handleToolClick = (tool) => {
    // If the selected tool is already active, reset activeTool to null first
    if (tool === activeTool) {
      setActiveTool(null);
  
      // Use setTimeout to re-activate the tool. This ensures React detects a state change.
      setTimeout(() => {
        setActiveTool(tool);
      }, 10); // Short delay before re-activating the tool
    } else {
      // If a different tool is selected, just activate it directly
      setActiveTool(tool);
    }
  };

  useEffect(() => {
    if (!map) return;

    let drawInteraction;
    if (activeTool) {
      drawInteraction = new Draw({
        source: map.getLayers().getArray()[1].getSource(),
        type: activeTool,
      });

      map.addInteraction(drawInteraction);

      drawInteraction.on('drawend', () => {
        // Deactivate drawing immediately after a draw ends
        map.removeInteraction(drawInteraction);
      });
    }

    // This cleanup function ensures the interaction is removed when the component unmounts or before adding a new interaction
    return () => {
      if (drawInteraction) {
        map.removeInteraction(drawInteraction);
      }
    };
  }, [activeTool, map]);

  useEffect(() => {
    if (!map) return;

    const overlay = map.getOverlays().getArray().find(o => o.getElement() === popupRef.current);
    
    const displayPopup = (evt) => {
      const feature = map.forEachFeatureAtPixel(evt.pixel, (ft) => ft, { hitTolerance: 5 });
      if (feature) {
        const geometry = feature.getGeometry();
        const coord = geometry.getCoordinates();
        let coordText;
        switch (geometry.getType()) {
					case "Point":
						coordText = `Coordinates: ${coord.join(", ")}`;
						break;
					case "LineString":
						coordText = `Line Coordinates: ${coord
							.map((c) => c.join(", "))
							.join("; ")}`;
						break;
					case "Polygon":
						coordText = `Polygon Coordinates: ${coord[0]
							.map((c) => c.join(", "))
							.join("; ")}`;
						break;
					default:
						coordText = "Unsupported geometry type";
				}
        overlay.setPosition(evt.coordinate);
        popupRef.current.innerHTML = coordText;
        popupRef.current.style.display = 'block';
      } else {
        popupRef.current.style.display = 'none'; // Hide the popup if no feature is clicked
      }
    };

    map.on('singleclick', displayPopup);

    return () => 
      map.un('singleclick', displayPopup);
    
  }, [map]);

  return (
    <>
      <div
        style={{
          position: 'absolute',
          top: '10px',
          left: '50%',
          transform: 'translateX(-50%)',
          zIndex: 1000,
        }}
      >
        <button onClick={() => handleToolClick('Point')}>Point</button>
        <button onClick={() => handleToolClick('LineString')}>Line</button>
        <button onClick={() => handleToolClick('Polygon')}>Polygon</button>
      </div>
      <div ref={mapRef} style={{ width: '100%', height: '100vh' }}></div>
      <div ref={popupRef} style={{ background: 'white', boxShadow: '0 1px 4px rgba(0,0,0,0.2)', padding: '15px', borderRadius: '10px', position: 'absolute', display: 'none' }}></div>
    </>
  );
};

export default MapComponent;
