////////////////////////////////////////////////////////////////////
// Copyright (C) 2014, SafeNet, Inc. All rights reserved.
//
//
//
//
////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Specialized;
using System.Text;
using Aladdin.HASP;
using UnityEngine;
    /// <summary>
    /// </summary>
    public class HaspDemo
    {
        protected StringCollection stringCollection;

        public const string localScope = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> " +
                                                                            "<haspscope> " +
                                                                            "   <license_manager hostname =\"localhost\" /> " +
                                                                            "</haspscope>";

        public const string defaultScope = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> " +
                                                                            "<haspscope/> ";

        private string scope= "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> " +
                                                                            "<haspscope> " +
                                                                            "   <license_manager hostname =\"localhost\" /> " +
                                                                            "</haspscope>";

    public const int dynamicMemoryFileId = 1;

        private const int dynamicMemoryBufferSize = 32;

        /// <summary>
        /// Constructor
        /// Intializes the object.
        /// </summary>
        public HaspDemo()
        {
            // keep a reference to the TextBox which
            // shall dump the results of the operations.
           

            // next could be considered ugly.
            // build up a string collection holding
            // the status codes in a human readable manner.
            string[] stringRange = new string[]
            {
                "Success.",
                "Invalid memory address.",
                "Unknown/invalid feature id option.",
                "Memory allocation failed.",
                "Too many open features.",
                "Feature access denied.",
                "Incompatible feature.",
                "Sentinel key not found.",
                "En-/decryption length too short.",
                "Invalid handle.",
                "Invalid file id / memory descriptor.",
                "Driver or support daemon version too old.",
                "Real time support not available.",
                "Generic error from host system call.",
                "Hardware key driver not found.",
                "Unrecognized info format.",
                "Request not supported.",
                "Invalid update object.",
                "Key with requested id was not found.",
                "Update data consistency check failed.",
                "Update not supported by this key.",
                "Update counter mismatch.",
                "Invalid vendor code.",
                "Requested encryption algorithm not supported.",
                "Invalid date / time.",
                "Clock out of power.",
                "Update requested ack., but no area to return it.",
                "Terminal services (remote terminal) detected.",
                "Feature type not implemented.",
                "Unknown algorithm.",
                "Signature check failed.",
                "Feature not found",
                "Trace log not enabled.",
                "Communication error between application and local LM",
                "Vendor code unknown to API library (run apigen to make it known)",
                "Invalid XML spec",
                "Invalid XML scope",
                "Too many keys connected",
                "Too many users",
                "Broken session",
                "Communication error between local and remote LM",
                "The feature is expired",
                "Sentinel LM version too old",
                "Sentinel SL secure storage I/O error or USB request error",
                "Update installation not allowed",
                "System time has been tampered",
                "Secure channel communication error",
                "Secure storage contains garbage",
                "Vendor lib cannot be found",
                "Vendor lib cannot be loaded",
                "No feature matching scope found",
                "Virtual machine detected",
                "Sentinel update incompatible with this hardware; Sentinel key is locked to other hardware",
                "Login denied because of user restrictions",
                "Update was already installed",
                "Another update must be installed first",
                "Vendor library version too old",
                "Upload error",
                "Invalid XML recipient parameter",
                "Invalid XML action parameter",
                "Scope does not select a unique Product",
                "Invalid Product information",
                "Update can only be applied to recipient which was specified",
                "Invalid duration",
                "Cloned Sentinel SL secure storage detected",
                "Specified V2C update already installed in the LM",
                "Specified HASP ID is in inactive state",
                "No detachable feature exists",
                "Scope does not specify a unique host",
                "Rehost is not allowed for any license",
                "License is rehosted to other machine",
                "Old rehost license try to apply",
                "File not found or access denied",
                "Extension of license not allowed",
                "Detach of license is not allowed",
                "Rehost of license is not allowed",
                "Operation now allowed as container has detached license",
                "Recipient of the requested operation is older than expected",
                "Secure storage ID mismatch",
                "Duplicate Hostname found while key contains Hostname Fingerprinting",
                "The Sentinel License Manager is required for this operation"
            };

            stringCollection = new StringCollection();
            stringCollection.AddRange(stringRange);

            for (int n = stringCollection.Count; n < 400; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringRange = new string[]
            {
                "A required API dynamic library was not found",
                "The found and assigned API dynamic library could not be verified",
            };

            stringCollection.AddRange(stringRange);

            for (int n = stringCollection.Count; n < 500; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringRange = new string[]
            {
                "Calling invalid object.",
                "A parameter is invalid.",
                "Already logged in.",
                "Already logged out."
            };

            stringCollection.AddRange(stringRange);

            for (int n = stringCollection.Count; n < 525; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(525, "Unable to excecute/complete the operation.");

            for (int n = stringCollection.Count; n < 600; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(600, "No classic memory extension block available.");

            for (int n = stringCollection.Count; n < 650; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(650, "Invalid port type.");
            stringCollection.Insert(651, "Invalid port.");
            stringCollection.Insert(652, "Dot-Net DLL found broken.");

            for (int n = stringCollection.Count; n < 698; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(698, "Capability is not available.");
            stringCollection.Insert(699, "Internal API error.");

            for (int n = stringCollection.Count; n < 2001; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(2001, "Reserved for Sentinel helper libraries.");

            for (int n = stringCollection.Count; n < 3001; n++)
            {
                stringCollection.Insert(n, "");
            }

            stringCollection.Insert(3001, "Reserved for Sentinel Activation API.");
        }


        /// <summary>
        /// Dumps a bunch of bytes into the referenced TextBox.
        /// </summary>
        public void DumpBytes(byte[] bytes)
        {
            Verbose("Dumping data (max. 64 Bytes):");

            for (int index = 0; index < bytes.Length; index++)
            {
                //if (0 == (index % 8))
                    //FormHaspDemo.WriteToTextbox(textHistory,(0 == index) ? "          " : "\r\n          ");

                Debug.Log( "0x" + bytes[index].ToString("X2") + " ");

            // for performance reason we only dump 64 bytes
            if (63 <= index)
            {
                Debug.Log("\r\n          ...");
                break;
            }
        }

        Verbose("");
        }


        /// <summary>
        /// Demonstrates the usage of the AES encryption and
        /// decryption methods.
        /// </summary>
        public void EncryptDecryptDemo(Hasp hasp)
        {
            // sanity check
            if ((null == hasp) || !hasp.IsLoggedIn())
                return;

            Verbose("Encrypt/Decrypt Demo");

            // the string to be encryted/decrypted.
            string text = "Sentinel LDK is great";
            Verbose("Encrypting \"" + text + "\"");

            // convert the string into a byte array.
            byte[] data = UTF8Encoding.Default.GetBytes(text);

            // encrypt the data.
            HaspStatus status = hasp.Encrypt(data);
            ReportStatus(status);

            if (HaspStatus.StatusOk == status)
            {
                text = UTF8Encoding.Default.GetString(data);
                Verbose("Encrypted string: \"" + text + "\"");
                Verbose("");
                Verbose("Decrypting \"" + text + "\"");

                // decrypt the data.
                // on success we convert the data back into a
                // human readable string.
                status = hasp.Decrypt(data);
                ReportStatus(status);

                if (HaspStatus.StatusOk == status)
                {
                    text = UTF8Encoding.Default.GetString(data);
                    Verbose("Decrypted string: \"" + text + "\"");
                }
            }

            Verbose("");

            // Second choice - encrypting a string using the
            // native .net API
            text = "Encrypt/Decrypt String";
            Verbose("Encrypting \"" + text + "\"");

            status = hasp.Encrypt(ref text);
            ReportStatus(status);

            if (HaspStatus.StatusOk == status)
            {
                Verbose("Encrypted string: \"" + text + "\"");

                Verbose("");
                Verbose("Decrypting \"" + text + "\"");

                status = hasp.Decrypt(ref text);
                ReportStatus(status);

                if (HaspStatus.StatusOk == status)
                    Verbose("Decrypted string: \"" + text + "\"");
            }

            Verbose("");
        }

        /// <summary>
        /// Prints the footer into the referenced TextBox.
        /// </summary>
        public virtual void Footer()
        {
            // FormHaspDemo.WriteToTextbox(textHistory, "API Demo completed\r\n");
            Debug.Log("API Demo completed\r\n");
        }


        /// <summary>
        /// Writes the Demo header into the referenced TextBox.
        /// </summary>
        public virtual void Header()
        {
            //if (textHistory != null && 0xFFFF < textHistory.TextLength)
            //    textHistory.Clear();

            //FormHaspDemo.WriteToTextbox(textHistory,
            //    "____________________________________________________________\r\n" +
            //    string.Format("API Demo started ({0})\r\n\r\n", DateTime.Now.ToString()));
        }


        /// <summary>
        /// Demonstrates how to perform a login and an automatic
        /// logout using C#'s scope clause.
        /// </summary>
        public void LoginDefaultAutoDemo()
        {
            Verbose("Login Demo with Default Feature (HaspFeature.Default)");

            HaspFeature feature = HaspFeature.Default;

            // this will perform a logout and object disposal
            // when the using scope is left.
            using (Hasp hasp = new Hasp(feature))
            {
                HaspStatus status = hasp.Login(VendorCode.Code, scope);
                ReportStatus(status);

                Verbose("Object going out of scope - System will call Dispose");
            }

            Verbose("");
        }


        /// <summary>
        /// Demonstrates how to login into a key using the
        /// default feature. The default feature is ALWAYS
        /// available in every key.
        /// </summary>
        public Hasp LoginDefaultDemo()
        {
            Verbose("Login Demo with Default Feature (HaspFeature.Default)");

            HaspFeature feature = HaspFeature.Default;

            Hasp hasp = new Hasp(feature);

            HaspStatus status = hasp.Login(VendorCode.Code, scope);
            ReportStatus(status);
            Verbose("");

            // Please note that there is no need to call
            // a logout function explicitly - although it is
            // recommended. The garbage collector will perform
            // the logout when disposing the object.
            // If you need more control over the logout procedure
            // perform one of the more advanced tasks.
            return hasp.IsLoggedIn() ? hasp : null;
        }


        /// <summary>
        /// Demonstrates how to login using a feature id.
        /// </summary>
        public Hasp LoginDemo(HaspFeature feature)
        {
            //Verbose("Login Demo with Feature: " +
            //        feature.FeatureId.ToString() +
            //        (feature.IsProgNum ? " (Program Number)" : ""));

            // create a key object using a feature
            // and perform a login using the vendor code.
            Hasp hasp = new Hasp(feature);

            HaspStatus status = hasp.Login(VendorCode.Code, scope);
            ReportStatus(status);

            //Verbose("");

            return hasp.IsLoggedIn() ? hasp : null;
        }


        /// <summary>
        /// Demonstrates how to perform a login using the default
        /// feature and how to perform an automatic logout
        /// using the Hasp's Dispose method.
        /// </summary>
        public void LoginDisposeDemo()
        {
            Verbose("Login/Dispose Demo with Default Feature (HaspFeature.Default)");

            HaspFeature feature = HaspFeature.Default;

            Hasp hasp = new Hasp(feature);

            HaspStatus status = hasp.Login(VendorCode.Code, scope);
            ReportStatus(status);

            Verbose("Disposing object - will perform an automatic logout");
            hasp.Dispose();

            Verbose("");
        }


        /// <summary>
        /// Demonstrates how to perform a login and a logout
        /// using the default feature.
        /// </summary>
        public void LoginLogoutDefaultDemo()
        {
           

            HaspFeature feature = HaspFeature.Default;

         
            Hasp hasp = new Hasp(feature);

            HaspStatus status = hasp.Login(VendorCode.Code, scope);
            ReportStatus(status);

            if (HaspStatus.StatusOk == status)
            {
             
                status = hasp.Logout();

                ReportStatus(status);
            }

            // recommended, but not mandatory
            // this call ensures that all resources to the key
            // are freed immediately.
            hasp.Dispose();
     
        }


        /// <summary>
        /// Performs a logout operation on the key.
        /// </summary>
        public void LogoutDemo(ref Hasp hasp)
        {
            // sanity check
            if ((null == hasp) || !hasp.IsLoggedIn())
                return;

            Verbose("Logout Demo");

            HaspStatus status = hasp.Logout();
            ReportStatus(status);

            // get rid of the key immediately.
            hasp.Dispose();
            hasp = null;
            Verbose("");
        }


        /// <summary>
        /// Demonstrates how to access Dynamic memory
        /// available in Sentinel HL (Driverless configuration)
        /// key. Use the defined Dynamic memory's file id to access it.
        /// </summary>
        public void ReadWriteDynamicMemory(Hasp hasp, int fileId)
        {
            // sanity check
            if ((null == hasp) || !hasp.IsLoggedIn())
                return;

            Verbose("Read/Write Dynamic Memory Demo");

            // Get a file object to a key's memory file.
            // please note: the file object is tightly connected
            // to its key object. logging out from a key also
            // invalidates the file object.
            // doing the following will result in an invalid
            // file object:
            // hasp.login(...)
            // HaspFile file = hasp.GetFile();
            // hasp.logout();
            // Debug.Assert(file.IsValid()); will assert
            HaspFile file = hasp.GetFile(fileId);
            if (!file.IsLoggedIn())
            {
                // Not logged into a key - nothing left to do.
                Verbose("Failed to get file object\r\n");
                return;
            }

            string fileIdhex = fileId.ToString("X");
            Verbose("Reading contents of dynamic memory fileid: 0x" + fileIdhex);


            Verbose("Retrieving the size ");

            // get the file size
            int size = 0;
            HaspStatus status = file.FileSize(ref size);
            ReportStatus(status);
            if (HaspStatus.InvalidFile == status)
            {
                // The specified dynamic memory fileid doesn't exists.
                Verbose("dynamic memory fileid 0x" + fileIdhex + "doesn't exists on the key");
                Verbose("");
                return;
            }

            if (HaspStatus.StatusOk != status)
            {
                Verbose("");
                return;
            }

            Verbose("Size of the dynamic memory fileid is: " + size.ToString() + " Bytes");

            if (size != 0) // skip if no dynamic memory exist or is of size zero
            {
                if (size > dynamicMemoryBufferSize)
                    size = dynamicMemoryBufferSize;

                // read the contents of the file into a buffer
                byte[] bytes = new byte[size];

                Verbose("Reading data");
                status = file.Read(bytes, 0, bytes.Length);
                ReportStatus(status);

                if (HaspStatus.StatusOk != status)
                {
                    Verbose("");
                    return;
                }

                DumpBytes(bytes);

                Verbose("Writing data");

                // now let's write some data into the file
                byte[] newBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

                status = file.Write(newBytes, 0, newBytes.Length);
                ReportStatus(status);
                if (HaspStatus.StatusOk != status)
                {
                    Verbose("");
                    return;
                }

                DumpBytes(newBytes);

                // and read them again
                Verbose("Reading written data");
                status = file.Read(newBytes, 0, newBytes.Length);
                ReportStatus(status);
                if (HaspStatus.StatusOk == status)
                    DumpBytes(newBytes);

                // restore the original contents
                file.Write(bytes, 0, bytes.Length);
                Verbose("");
            }
        }

    public void ReadDynamicMemory(Hasp hasp, int fileId)
    {
        // sanity check
        if ((null == hasp) || !hasp.IsLoggedIn())
            return;
        HaspFile file = hasp.GetFile(fileId);
        if (!file.IsLoggedIn())
        {
            return;
        }

        string fileIdhex = fileId.ToString("X");
        int size = 0;
        HaspStatus status = file.FileSize(ref size);
        ReportStatus(status);
        if (HaspStatus.InvalidFile == status)
        {
 
            return;
        }

        if (HaspStatus.StatusOk != status)
        {
        
            return;
        }

     
        if (size != 0) // skip if no dynamic memory exist or is of size zero
        {
            if (size > dynamicMemoryBufferSize)
                size = dynamicMemoryBufferSize;

            // read the contents of the file into a buffer
            byte[] bytes = new byte[size];

            status = file.Read(bytes, 0, bytes.Length);
       
            if (HaspStatus.StatusOk != status)
            {
             
                return;
            }

            DumpBytes(bytes);

            //Verbose("Writing data");

            //// now let's write some data into the file
            //byte[] newBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

            //status = file.Write(newBytes, 0, newBytes.Length);
            //ReportStatus(status);
            //if (HaspStatus.StatusOk != status)
            //{
            //    Verbose("");
            //    return;
            //}

            //DumpBytes(newBytes);

            //// and read them again
            //Verbose("Reading written data");
            //status = file.Read(newBytes, 0, newBytes.Length);
            //ReportStatus(status);
            //if (HaspStatus.StatusOk == status)
            //    DumpBytes(newBytes);

            //// restore the original contents
            //file.Write(bytes, 0, bytes.Length);
            //Verbose("");
        }
    }

   

    public string ReadSplitStr(string value,int index,int length)
    {
        if (value.Length < length)
        {
            return value.Substring(index,value.Length);
        }
        return value.Substring(index,length);
    }

    public string ReadToStr(Hasp hasp, HaspFileId fileId)
    {
        string dd = "";
        try
        {
            HaspFile file = hasp.GetFile(fileId);
            file.Read(ref dd);
            return dd;
        }
        
        catch
        {
            return null;
        }
    }

    public bool WriteMessageFirst(Hasp hasp, HaspFileId fileId,string str)
    {
        try
        {
            HaspFile file = hasp.GetFile(fileId);
            file.Write(str);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Demonstrates how to perform read and write
    /// operations on a key's file
    /// </summary>
    public void ReadWriteDemo(Hasp hasp, HaspFileId fileId)
        {
            // sanity check
            if ((null == hasp) || !hasp.IsLoggedIn())
        {
            Debug.Log("llllllllllllllllllllll");
            return;
        }
                

            Verbose("Read/Write Demo");

            // Get a file object to a key's memory file.
            // please note: the file object is tightly connected
            // to its key object. logging out from a key also
            // invalidates the file object.
            // doing the following will result in an invalid
            // file object:
            // hasp.login(...)
            // HaspFile file = hasp.GetFile();
            // hasp.logout();
            // Debug.Assert(file.IsValid()); will assert
            HaspFile file = hasp.GetFile(fileId);
            if (!file.IsLoggedIn())
            {
                // Not logged into a key - nothing left to do.
                Verbose("Failed to get file object\r\n");
                return;
            }

            Verbose("Reading contents of file: " + file.FileId.ToString());

            Verbose("Retrieving the size of the file");

            // get the file size
            int size = 0;
            HaspStatus status = file.FileSize(ref size);
            ReportStatus(status);

            if (HaspStatus.StatusOk != status)
            {
                Verbose("");
                return;
            }

            Verbose("Size of the file is: " + size.ToString() + " Bytes");

            // read the contents of the file into a buffer
            byte[] bytes = new byte[size];

            Verbose("Reading data");
            status = file.Read(bytes, 0, bytes.Length);
            ReportStatus(status);

            if (HaspStatus.StatusOk != status)
            {
                Verbose("");
                return;
            }

            DumpBytes(bytes);

            Verbose("Writing to file");

            // now let's write some data into the file
            byte[] newBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7 };

            status = file.Write(newBytes, 0, newBytes.Length);
            ReportStatus(status);
            if (HaspStatus.StatusOk != status)
            {
                Verbose("");
                return;
            }

            DumpBytes(newBytes);

            // and read them again
            Verbose("Reading written data");
            status = file.Read(newBytes, 0, newBytes.Length);
            ReportStatus(status);
            if (HaspStatus.StatusOk == status)
                DumpBytes(newBytes);

            // restore the original contents
            file.Write(bytes, 0, bytes.Length);
            Verbose("");
        }


        /// <summary>
        /// Demonstrates how to read and write to/from a key's
        /// file at a certain file position
        /// </summary>
        public void ReadWritePosDemo(Hasp hasp, HaspFileId fileId)
        {
            // sanity check
            if ((null == hasp) || !hasp.IsLoggedIn())
                return;

            Verbose("GetFileSize/FilePos Demo");

            // firstly get a file object to a key's file.
            HaspFile file = hasp.GetFile(fileId);
            if (!file.IsLoggedIn())
            {
                // Not logged into key - nothing left to do.
                Verbose("Failed to get file object\r\n");
                return;
            }

            Verbose("Reading contents of file: " + file.FileId.ToString());
            Verbose("Retrieving the size of the file");

            // we want to write an int at the end of the file.
            // therefore we are going to
            // - get the file's size
            // - set the object's read and write position to
            //   the appropriate offset.
            int size = 0;
            HaspStatus status = file.FileSize(ref size);
            ReportStatus(status);

            if (HaspStatus.StatusOk != status)
            {
                Verbose("");
                return;
            }

            Verbose("Size of the file is: " + size.ToString() + " Bytes");
            Verbose("Setting file position to last int and reading value");

            // set the file pos to the end minus the size of int
            file.FilePos = size - HaspFile.TypeSize(typeof(int));

            // now read what's there
            int aValue = 0;
            status = file.Read(ref aValue);
            ReportStatus(status);

            if (HaspStatus.StatusOk != status)
            {
                Verbose("");
                return;
            }

            Verbose("Writing to file: 0x" + int.MaxValue.ToString("X2"));

            // write some data.
            status = file.Write(int.MaxValue);
            ReportStatus(status);

            if (HaspStatus.StatusOk != status)
            {
                Verbose("");
                return;
            }

            // read back the written value.
            int newValue = 0;
            Verbose("Reading written data");
            status = file.Read(ref newValue);

            ReportStatus(status);
            if (HaspStatus.StatusOk == status)
                Verbose("Data read: 0x" + newValue.ToString("X2"));

            // restore the original data.
            file.Write(aValue);
            Verbose("");
        }


        /// <summary>
        /// Dumps an operation status into the
        /// referenced TextBox.
        /// </summary>
        public void ReportStatus(HaspStatus status)
        {
            Debug.Log(string.Format("     Result: {0} (HaspStatus::{1})\r\n",stringCollection[(int)status],status.ToString()));

            //FormHaspDemo.WriteToTextbox(textHistory,
            //    string.Format("     Result: {0} (HaspStatus::{1})\r\n",
            //                        stringCollection[(int)status],
            //                        status.ToString()));

            //if (textHistory != null)
            //{
            //    if (HaspStatus.StatusOk == status)
            //        textHistory.Refresh();
            //    else
            //        textHistory.Parent.Refresh();
            //}
        }


        /// <summary>
        /// Demonstrates how to get the real time clock of
        /// a key when available.
        /// </summary>
        public void RtcDemo(Hasp hasp)
        {
            // sanity check
            if ((null == hasp) || !hasp.IsLoggedIn())
                return;

            Verbose("Reading the Real Time Clock Demo");

            DateTime time = DateTime.Now;
            HaspStatus status = hasp.GetRtc(ref time);
            ReportStatus(status);

            if (HaspStatus.StatusOk == status)
                Verbose("Real Time Clock is " + time.ToString());

            Verbose("");
        }


        /// <summary>
        /// Runs the API demo.
        /// </summary>
        public void RunDemo(string scope)
        {
            try
            {
                this.scope = scope;

                Header();

                // Demonstrate the different login methods
                LoginDefaultAutoDemo();
                LoginLogoutDefaultDemo();
                LoginDisposeDemo();

                // Demonstrates how to get a list of available features
                GetInfoDemo();

                // run the API demo using the default feature
                // (ALWAYS present in every key)
                Hasp hasp = LoginDefaultDemo();
                SessionInfoDemo(hasp);
                ReadWriteDemo(hasp, HaspFileId.ReadWrite);
                ReadWritePosDemo(hasp, HaspFileId.ReadWrite);
                EncryptDecryptDemo(hasp);
                RtcDemo(hasp);

                // Accessing Dynamic memory available in Sentinel HL (Driverless configuration) Key
                ReadWriteDynamicMemory(hasp, HaspDemo.dynamicMemoryFileId);

                LogoutDemo(ref hasp);

                int[] features =
                {
                    HaspFeature.FromFeature(1).Feature,
                    HaspFeature.FromFeature(3).Feature,
                    HaspFeature.FromFeature(42).Feature,
                    HaspFeature.FromFeature(101).Feature
                };

                // run the API demo using various feature ids
                for (int index = 0; index < features.Length; index++)
                {
                    hasp = LoginDemo(new HaspFeature(features[index]));
                    SessionInfoDemo(hasp);
                    ReadWriteDemo(hasp, HaspFileId.ReadWrite);
                    ReadWritePosDemo(hasp, HaspFileId.ReadWrite);
                    EncryptDecryptDemo(hasp);
                    RtcDemo(hasp);
                    LogoutDemo(ref hasp);
                }
                Footer();
            }
            catch (Exception ex)
            {
                //if (textHistory == null)
                //    Console.WriteLine(ex.Message);
                //else
                //    System.Windows.Forms.MessageBox.Show(ex.Message,
                //                                     "Exception",
                //                                      System.Windows.Forms.MessageBoxButtons.OK);
            }
        }


        /// <summary>
        /// Demonstrates how to use to retrieve a XML containing all available features.
        /// </summary>
        public void GetInfoDemo()
        {
            string queryFormat = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>" +
                                                      "<haspformat root=\"hasp_info\">" +
                                                        " <feature>" +
                                                        "  <attribute name=\"id\" />" +
                                                        "  <element name=\"license\" />" +
                                                        " </feature>" +
                                                        "</haspformat>";

            Verbose("Get Information Demo");

            Verbose("Retrieving Feature Information");

            string info = null;

            HaspStatus status = Hasp.GetInfo(scope, queryFormat, VendorCode.Code, ref info);

            ReportStatus(status);
            if (HaspStatus.StatusOk == status)
            {
                Verbose("Fature Information:");
                Verbose(info.Replace("\n", "\r\n          "));
            }
            else
                Verbose("");
        }


        /// <summary>
        /// Demonstrates how to retrieve information from a key.
        /// </summary>
        public void SessionInfoDemo(Hasp hasp)
        {
            // sanity check
            if ((null == hasp) || !hasp.IsLoggedIn())
                return;

            Verbose("Get Session Information Demo");

            Verbose("Retrieving Key Information");

            // firstly we will retrieve the key info.
            string info = null;
            HaspStatus status = hasp.GetSessionInfo(Hasp.KeyInfo,
                                                    ref info);
            ReportStatus(status);
            if (HaspStatus.StatusOk == status)
            {
                Verbose("Key Information:");
                Verbose(info.Replace("\n", "\r\n          "));
            }
            else
                Verbose("");

            Verbose("Retrieving Session Information");

            // next the session info.
            status = hasp.GetSessionInfo(Hasp.SessionInfo, ref info);
            ReportStatus(status);
            if (HaspStatus.StatusOk == status)
            {
                Verbose("Session Information:");
                Verbose(info.ToString());
            }
            else
                Verbose("");

            Verbose("Retrieving Update Information");

            // last the update information.
            status = hasp.GetSessionInfo(Hasp.UpdateInfo, ref info);
            ReportStatus(status);
            if (HaspStatus.StatusOk == status)
            {
                Verbose("Update Information:");
                Verbose(info.ToString());
            }
            else
                Verbose("");
        }


        /// <summary>
        /// Writes some descriptive text into the
        /// referenced TextBox.
        /// </summary>
        public void Verbose(string text)
        {
            if (null == text)
                return;

            //FormHaspDemo.WriteToTextbox(textHistory, "     " + text + "\r\n");
            Debug.Log(text + "\r\n");
        }
    }

