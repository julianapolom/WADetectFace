using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WADetectFace.Services
{
    public class ServiceFace
    {
        private const string faceKey = "0fce65024eae4ccf8372d9813de1db01";
        private const string faceEndPoint = "https://cara1.cognitiveservices.azure.com/";
        private const string RECOGNITION_MODEL3 = RecognitionModel.Recognition01;

        /// <summary>
        /// Método que realizar la autenticación con el servicio Face y detecta el rostro
        /// </summary>
        /// <param name="imagen">Imagen a analizar</param>
        /// <returns>Listado de edades</returns>
        public async Task<List<string>> DetectarRostro(Stream imagen)
        {
            return await DetectFaceExtract(Authenticate(faceEndPoint, faceKey), RECOGNITION_MODEL3, imagen);
        }

        /// <summary>
        /// Autenticación
        /// </summary>
        /// <param name="endpoint">Ruta de acceso al servicio Face</param>
        /// <param name="key">Llave para consumir el servicio Face</param>
        /// <returns>Cliente de Face</returns>
        private static IFaceClient Authenticate(string endpoint, string key)
        {
            return new FaceClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
        }

        /// <summary>
        /// Extraer datos del rostro
        /// </summary>
        /// <param name="client">Cliente autenticado</param>
        /// <param name="recognitionModel">Modelo de reconocimiento a utilizar</param>
        /// <param name="imagen">Objeto de la imagen</param>
        /// <returns>Lista de edades</returns>
        private static async Task<List<string>> DetectFaceExtract(IFaceClient client, string recognitionModel, Stream imagen)
        {
            try
            {
                IList<DetectedFace> detectedFaces = null;
                List<string> AgeList = new List<string>();

                // Detect faces with all attributes from image url.
                detectedFaces = await client.Face.DetectWithStreamAsync(imagen,
                                returnFaceAttributes: new List<FaceAttributeType?> { FaceAttributeType.Accessories, FaceAttributeType.Age },
                                   detectionModel: DetectionModel.Detection01,
                                   recognitionModel: recognitionModel);

                foreach (var item in detectedFaces)
                {
                    AgeList.Add(item.FaceAttributes?.Age?.ToString());
                }

                return AgeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}