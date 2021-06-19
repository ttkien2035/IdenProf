import os
import urllib.request
from imageai.Prediction.Custom import CustomImagePrediction
from app import app, UPLOAD_FOLDER
#from FirstCustomImageRecognition import *
from flask import Flask, request, redirect, jsonify
from werkzeug.utils import secure_filename

ALLOWED_EXTENSIONS = set(['txt', 'pdf', 'png', 'jpg', 'jpeg', 'gif'])
execution_path = os.getcwd()

prediction = CustomImagePrediction()
prediction.setModelTypeAsResNet()
prediction.setModelPath("idenprof_061-0.7933.h5")
prediction.setJsonPath("idenprof_model_class.json")
prediction.loadModel(num_objects=10)

def allowed_file(filename):
	return '.' in filename and filename.rsplit('.', 1)[1].lower() in ALLOWED_EXTENSIONS

def CustomImage(LOCAL_FOLDER, result_count):
    objects_list = []
    predictions, probabilities = prediction.predictImage(LOCAL_FOLDER, result_count=result_count)
    for eachPrediction, eachProbability in zip(predictions, probabilities):
        data = eachPrediction + " : " + str(eachProbability)
        objects_list.append(data)
    return objects_list

# for eachPrediction, eachProbability in zip(predictions, probabilities):
#    resp = jsonify(eachPrediction , " : " , eachProbability)
# resp = jsonify(prediction.predictImage(filename, result_count=10))

@app.route('/file-upload', methods=['POST'])
def upload_file():
	# check if the post request has the file part
	if 'file' not in request.files:
		resp = jsonify({'message' : 'No file part in the request'})
		resp.status_code = 400
		return resp
	file = request.files['file']
	if file.filename == '':
		resp = jsonify({'message' : 'No file selected for uploading'})
		resp.status_code = 400
		return resp
	if file and allowed_file(file.filename) and request.form['result_count']:
		filename = secure_filename(file.filename)
		file.save(os.path.join(app.config['UPLOAD_FOLDER'], filename))
		resp = jsonify(CustomImage(UPLOAD_FOLDER + "/" + filename, request.form['result_count']))
		resp.status_code = 200
		return resp
	else:
		resp = jsonify({'message' : 'Allowed file types are txt, pdf, png, jpg, jpeg, gif'})
		resp.status_code = 400
		return resp

if __name__ == "__main__":
    app.run(debug=os.environ['DEBUG'] if 'DEBUG' in os.environ else False,
	    port=5000, threaded=True)

# host="192.168.100.45",