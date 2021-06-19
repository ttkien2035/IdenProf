from imageai.Prediction.Custom import CustomImagePrediction
import os

execution_path = os.getcwd()

prediction = CustomImagePrediction()
prediction.setModelTypeAsResNet()
prediction.setModelPath("idenprof_061-0.7933.h5")
prediction.setJsonPath("idenprof_model_class.json")
prediction.loadModel(num_objects=10)

predictions, probabilities = prediction.predictImage("Test\\chef-17.jpg", result_count=5)

for eachPrediction, eachProbability in zip(predictions, probabilities):
    print(eachPrediction , " : " , eachProbability)
