using UnityEngine;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using UnityEngine.Networking;

public class AWSManager : MonoBehaviour
{
    private static AWSManager _instance;
    public static AWSManager Instance
    {
        get
        {
            if (_instance == null)
                throw new UnityException("AWS Manager is NULL");
            return _instance;
        }
    }
    public GameObject targetImage;

    private AmazonS3Client _s3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if (_s3Client == null)
                _s3Client = new AmazonS3Client(new CognitoAWSCredentials("us-east-2:50315d2e-251d-4ec2-b4f7-85be6bec04b1", RegionEndpoint.USEast2), _S3Region);
            return _s3Client;
        }
    }

    private string S3Region = RegionEndpoint.USWest2.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }
    void Awake()
    {
        _instance = this;

        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        string target = "horse";
        ListObjectsRequest request = new ListObjectsRequest()
        {
            BucketName = "assetbundles11212"
        };

        S3Client.ListObjectsAsync(request, (responseObject) =>
        {
            if (responseObject.Exception == null)
            {
                bool assetFound = responseObject.Response.S3Objects.Any(obj => obj.Key == target);

                if (assetFound == true)
                {
                    Debug.Log("Asset Bundle Found!");
                    StartCoroutine(DownloadBundleRoutine());
                }
                else
                {
                    Debug.Log("Asset not found!");
                }
            }
            else
                Debug.Log("Error getting list of items from S3: " + responseObject.Exception);
        });

    }

    IEnumerator DownloadBundleRoutine()
    {
        string uri = "https://assetbundles11212.s3.us-west-2.amazonaws.com/horse";
        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(uri))
        {
            yield return request.SendWebRequest();

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

            GameObject horse = bundle.LoadAsset<GameObject>("horse");
            Instantiate(horse, targetImage.transform);
        }
    }
}
