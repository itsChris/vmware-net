﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Net;
using VMware.Vim;

namespace vmware_net
{
    public class Globals
    {
        public static string sUsername;
        public static string sPassword;
        public static string sViServer;
    }
    public partial class Default : System.Web.UI.Page
    {
        protected VimClient ConnectServer(string viServer, string viUser, string viPassword)
        {
            //
            // Establish a connetion to the provided sdk server
            //
            VimClient vimClient = new VimClient();
            ServiceContent vimServiceContent = new ServiceContent();
            UserSession vimSession = new UserSession();
            //
            // Connect over https to the /sdk page
            //
            vimClient.Connect("https://" + viServer.Trim() + "/sdk");
            vimSession = vimClient.Login(viUser, viPassword);
            vimServiceContent = vimClient.ServiceContent;

            return vimClient;
        }
        protected List<Datacenter> GetDataCenter(VimClient vimClient, string dcName = null)
        {
            List<Datacenter> lstDatacenters = new List<Datacenter>();
            List<EntityViewBase> appDatacenters = new List<EntityViewBase>();

            if (dcName == null)
            {
                appDatacenters = vimClient.FindEntityViews(typeof(Datacenter), null, null, null);
            }
            else
            {
                NameValueCollection dcFilter = new NameValueCollection();
                dcFilter.Add("name", dcName);
                appDatacenters = vimClient.FindEntityViews(typeof(Datacenter), null, dcFilter, null);
            }
            foreach (EntityViewBase appDatacenter in appDatacenters)
            {
                Datacenter thisDatacenter = (Datacenter)appDatacenter;
                lstDatacenters.Add(thisDatacenter);
            }

            return lstDatacenters;
        }
        protected List<Datastore> GetDataStore(VimClient vimClient, Datacenter selectedDC = null, string dsName = null)
        {
            List<Datastore> lstDatastores = new List<Datastore>();
            NameValueCollection dsFilter = new NameValueCollection();
            ManagedObjectReference DcMoRef = new ManagedObjectReference();

            if (dsName != null)
            {
                dsFilter.Add("name", dsName);
            }
            else
            {
                dsFilter = null;
            }

            if (selectedDC != null)
            {
                DcMoRef = selectedDC.MoRef;
            }
            else
            {
                DcMoRef = null;
            }

            List<EntityViewBase> appDatastores = vimClient.FindEntityViews(typeof(Datastore), DcMoRef, dsFilter, null);
            if (appDatastores != null)
            {
                foreach (EntityViewBase appDatastore in appDatastores)
                {
                    Datastore thisDatastore = (Datastore)appDatastore;
                    lstDatastores.Add(thisDatastore);
                }
                return lstDatastores;
            }
            else
            {
                return null;
            }
        }
        protected VmwareDistributedVirtualSwitch GetDvSwitch(VimClient vimClient, ManagedObjectReference dvportGroupSwitch)
        {
            ViewBase appSwitch = vimClient.GetView(dvportGroupSwitch, null);
            if (appSwitch != null)
            {
                VmwareDistributedVirtualSwitch thisDvSwitch = (VmwareDistributedVirtualSwitch)appSwitch;
                return thisDvSwitch;
            }
            else
            {
                return null;
            }
        }
        protected List<DistributedVirtualPortgroup> GetDVPortGroups(VimClient vimClient, Datacenter selectedDC = null, string pgName = null)
        {
            List<DistributedVirtualPortgroup> lstPortGroups = new List<DistributedVirtualPortgroup>();
            NameValueCollection pgFilter = new NameValueCollection();
            ManagedObjectReference DcMoRef = new ManagedObjectReference();

            if (pgName != null)
            {
                pgFilter.Add("name", pgName);
            }
            else
            {
                pgFilter = null;
            }

            if (selectedDC != null)
            {
                DcMoRef = selectedDC.MoRef;
            }
            else
            {
                DcMoRef = null;
            }

            List<EntityViewBase> appPortGroups = vimClient.FindEntityViews(typeof(DistributedVirtualPortgroup), DcMoRef, pgFilter, null);
            if (appPortGroups != null)
            {
                foreach (EntityViewBase appPortGroup in appPortGroups)
                {
                    DistributedVirtualPortgroup thisPortGroup = (DistributedVirtualPortgroup)appPortGroup;
                    lstPortGroups.Add(thisPortGroup);
                }
                return lstPortGroups;
            }
            else
            {
                return null;
            }
        }
        protected List<Network> GetPortGroups(VimClient vimClient, Datacenter selectedDC = null, string pgName = null)
        {
            List<Network> lstPortGroups = new List<Network>();
            NameValueCollection pgFilter = new NameValueCollection();
            ManagedObjectReference DcMoRef = new ManagedObjectReference();

            if (pgName != null)
            {
                pgFilter.Add("name", pgName);
            }
            else
            {
                pgFilter = null;
            }

            if (selectedDC != null)
            {
                DcMoRef = selectedDC.MoRef;
            }
            else
            {
                DcMoRef = null;
            }

            List<EntityViewBase> appPortGroups = vimClient.FindEntityViews(typeof(Network), DcMoRef, pgFilter, null);
            if (appPortGroups != null)
            {
                foreach (EntityViewBase appPortGroup in appPortGroups)
                {
                    Network thisPortGroup = (Network)appPortGroup;
                    lstPortGroups.Add(thisPortGroup);
                }
                return lstPortGroups;
            }
            else
            {
                return null;
            }
        }
        protected List<VirtualMachine> GetVirtualMachines(VimClient vimClient, Datacenter selectedDC = null, string vmName = null)
        {
            List<VirtualMachine> lstVirtualMachines = new List<VirtualMachine>();
            NameValueCollection vmFilter = new NameValueCollection();
            ManagedObjectReference DcMoRef = new ManagedObjectReference();

            if (vmName != null)
            {
                vmFilter.Add("name", vmName);
            }
            else
            {
                vmFilter = null;
            }

            if (selectedDC != null)
            {
                DcMoRef = selectedDC.MoRef;
            }
            else
            {
                DcMoRef = null;
            }

            List<EntityViewBase> appVirtualMachines = vimClient.FindEntityViews(typeof(VirtualMachine), DcMoRef, vmFilter, null);
            if (appVirtualMachines != null)
            {
                foreach (EntityViewBase appVirtualMachine in appVirtualMachines)
                {
                    VirtualMachine thisVirtualMachine = (VirtualMachine)appVirtualMachine;
                    lstVirtualMachines.Add(thisVirtualMachine);
                }
                return lstVirtualMachines;
            }
            else
            {
                return null;
            }
        }
        protected List<CustomizationSpecInfo> GetCustomizationSpecs(VimClient vimClient)
        {
            List<CustomizationSpecInfo> lstSpecInfo = new List<CustomizationSpecInfo>();
            CustomizationSpecManager specManager = (CustomizationSpecManager)vimClient.GetView(vimClient.ServiceContent.CustomizationSpecManager, null);
            if (specManager != null)
            {
                foreach (CustomizationSpecInfo specInfo in specManager.Info)
                {
                    lstSpecInfo.Add(specInfo);
                }
                return lstSpecInfo;
            }
            else
            {
                return null;
            }
        }
        protected CustomizationSpecItem GetCustomizationSpecItem(VimClient vimClient, string specName = null)
        {
            CustomizationSpecManager specManager = (CustomizationSpecManager)vimClient.GetView(vimClient.ServiceContent.CustomizationSpecManager, null);
            if (specManager != null)
            {
                CustomizationSpecItem itmCustomizationSpecItem = specManager.GetCustomizationSpec(specName);
                return itmCustomizationSpecItem;
            }
            else
            {
                return null;
            }
        }
        protected List<ClusterComputeResource> GetClusters(VimClient vimClient, string clusterName = null)
        {
            List<ClusterComputeResource> lstClusters = new List<ClusterComputeResource>();
            List<EntityViewBase> appClusters = new List<EntityViewBase>();

            if (clusterName == null)
            {
                appClusters = vimClient.FindEntityViews(typeof(ClusterComputeResource), null, null, null);
            }
            else
            {
                NameValueCollection clusterFilter = new NameValueCollection();
                clusterFilter.Add("name", clusterName);
                appClusters = vimClient.FindEntityViews(typeof(ClusterComputeResource), null, clusterFilter, null);
            }
            if (appClusters != null)
            {
                foreach (EntityViewBase appCluster in appClusters)
                {
                    ClusterComputeResource thisCluster = (ClusterComputeResource)appCluster;
                    lstClusters.Add(thisCluster);
                }
                return lstClusters;
            }
            else
            {
                return null;
            }
        }
        protected List<HostSystem> GetHosts(VimClient vimClient, string hostParent = null)
        {
            List<HostSystem> lstHosts = new List<HostSystem>();
            List<EntityViewBase> appHosts = new List<EntityViewBase>();
            if (hostParent == null)
            {
                appHosts = vimClient.FindEntityViews(typeof(HostSystem), null, null, null);
            }
            else
            {
                NameValueCollection hostFilter = new NameValueCollection();
                hostFilter.Add("parent", hostParent);

                appHosts = vimClient.FindEntityViews(typeof(HostSystem), null, hostFilter, null);
            }
            if (appHosts != null)
            {
                foreach (EntityViewBase appHost in appHosts)
                {
                    HostSystem thisHost = (HostSystem)appHost;
                    lstHosts.Add(thisHost);
                }
                return lstHosts;
            }
            else
            {
                return null;
            }
        }
        protected List<Datacenter> GetDcFromCluster(VimClient vimClient, string clusterParent)
        {
            List<Datacenter> lstDataCenters = new List<Datacenter>();
            NameValueCollection parentFilter = new NameValueCollection();
            parentFilter.Add("hostFolder", clusterParent);

            List<EntityViewBase> arrDataCenters = vimClient.FindEntityViews(typeof(Datacenter), null, parentFilter, null);
            if (arrDataCenters != null)
            {
                foreach (EntityViewBase arrDatacenter in arrDataCenters)
                {
                    Datacenter thisDatacenter = (Datacenter)arrDatacenter;
                    lstDataCenters.Add(thisDatacenter);
                }
                return lstDataCenters;
            }
            else
            {
                return null;
            }
        }
        protected List<ResourcePool> GetResPools(VimClient vimClient, string ClusterMoRefVal)
        {
            List<ResourcePool> lstResPools = new List<ResourcePool>();
            NameValueCollection clusterFilter = new NameValueCollection();
            clusterFilter.Add("parent", ClusterMoRefVal);

            List<EntityViewBase> arrResPools = vimClient.FindEntityViews(typeof(ResourcePool), null, clusterFilter, null);
            if (arrResPools != null)
            {
                foreach (EntityViewBase arrResPool in arrResPools)
                {
                    ResourcePool thisResPool = (ResourcePool)arrResPool;
                    lstResPools.Add(thisResPool);
                }
                return lstResPools;
            }
            else
            {
                return null;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //
            // Uncomment the following code to redirect this app to HTTPS once
            //  you have a valid cert installed on your IIS box.
            //
            //if (!Request.IsLocal && !Request.IsSecureConnection)
            //{
            //    string redirectUrl = Request.Url.ToString().Replace("http:", "https:");
            //    Response.Redirect(redirectUrl);
            //}
            //
            // Read in the URL of the Vmware SDK Server from web.config
            // Uncomment the code below to store the sdk servername in the web.config
            //
            //txtSdkServer.Text = WebConfigurationManager.AppSettings["viServer"].ToString();
        }
        protected void cmdConnect_Click(object sender, EventArgs e)
        {
            //
            // The code here establishes a connection to your Vmware Datacenter/Cluster/Standalone
            // server and populates the controls in the vm_panel.
            //
            Globals.sUsername = txtUsername.Text;
            Globals.sPassword = txtPassword.Text;
            //
            // Comment or remove the code below if you populate the sdk server from web.config
            //
            Globals.sViServer = txtSdkServer.Text;
            //
            // Establish a connection with the Vmware server
            //
            VimClient vimClient = ConnectServer(Globals.sViServer, Globals.sUsername, Globals.sPassword);
            //
            // Get a list of clones
            //
            List<VirtualMachine> lstVirtualMachines = GetVirtualMachines(vimClient, null, null);
            if (lstVirtualMachines != null)
            {
                foreach (VirtualMachine itmVirtualMachine in lstVirtualMachines)
                {
                    ListItem thisVirtualMachine = new ListItem();
                    thisVirtualMachine.Text = itmVirtualMachine.Name;
                    thisVirtualMachine.Value = itmVirtualMachine.MoRef.ToString();
                    cboSourceVms.Items.Add(thisVirtualMachine);
                }
            }
            //
            // Get a list of OS Customizations
            //
            List<CustomizationSpecInfo> lstSpecs = GetCustomizationSpecs(vimClient);
            if (lstSpecs != null)
            {
                foreach (CustomizationSpecInfo itmSpec in lstSpecs)
                {
                    ListItem thisSpec = new ListItem();
                    thisSpec.Text = itmSpec.Name;
                    thisSpec.Value = itmSpec.Name + "." + itmSpec.Type;
                    cboCustomizations.Items.Add(thisSpec);
                }
            }
            //
            // Get a list of clusters
            //
            List<ClusterComputeResource> lstClusters = GetClusters(vimClient);
            foreach (ClusterComputeResource itmCluster in lstClusters)
            {
                ListItem thisCluster = new ListItem();
                thisCluster.Text = itmCluster.Name;
                thisCluster.Value = itmCluster.MoRef.Value;
                cboClusters.Items.Add(thisCluster);
            }
            //
            // Need to get at the Datacenter
            //
            List<Datacenter> lstDatacenters = GetDcFromCluster(vimClient, lstClusters[0].Parent.Value);
            Datacenter itmDatacenter = lstDatacenters[0];
            //
            // Get a list of datastores
            //
            List<Datastore> lstDatastores = GetDataStore(vimClient, itmDatacenter);
            lstDatastores = lstDatastores.OrderByDescending(thisStore => thisStore.Info.FreeSpace).ToList();
            foreach (Datastore itmDatastore in lstDatastores)
            {
                ListItem thisDatastore = new ListItem();
                thisDatastore.Text = itmDatastore.Name;
                thisDatastore.Value = itmDatastore.MoRef.Value;
                cboDatastores.Items.Add(thisDatastore);
            }
            //
            // Get a list of network portgroups
            //
            List<DistributedVirtualPortgroup> lstDVPortGroups = GetDVPortGroups(vimClient, itmDatacenter);
            if (lstDVPortGroups != null)
            {
                foreach (DistributedVirtualPortgroup itmPortGroup in lstDVPortGroups)
                {
                    ListItem thisPortGroup = new ListItem();
                    thisPortGroup.Text = itmPortGroup.Name;
                    thisPortGroup.Value = itmPortGroup.MoRef.ToString();
                    cboPortGroups.Items.Add(thisPortGroup);
                }
            }
            //
            // Get a list of Resource Pools
            //
            List<ResourcePool> lstResPools = GetResPools(vimClient, cboClusters.SelectedItem.Value);
            if (lstResPools != null)
            {
                foreach (ResourcePool itmResPool in lstResPools)
                {
                    ListItem thisResPool = new ListItem();
                    thisResPool.Text = itmResPool.Name;
                    thisResPool.Value = itmResPool.MoRef.ToString();
                    cboResourcePools.Items.Add(thisResPool);
                }
            }
            //
            // Close the session with the server
            //
            vimClient.Disconnect();
            //
            // Hide the login controls and show the vm controls
            //
            Login_Panel.Visible = false;
            Vm_Panel.Visible = true;
        }
        protected void cmdProvision_Click(object sender, EventArgs e)
        {
            //
            // Ready to provision
            //
            //
            // Need a random number to pick a random virtual host to place the new vm on
            //
            Random rand = new Random();
            //
            // Validate user entries
            //
            //
            // Do we have a value for the target vm?
            //
            if (txtTargetVm.Text == null || txtTargetVm.Text == "")
            {
                txtResults.Text = "Please enter a name for the virtual machine.";
                return;
            }
            //
            // Has a valid IP address been entered?
            //
            IPAddress theIp;
            bool ipResult = IPAddress.TryParse(txtIpAddress.Text, out theIp);
            if (ipResult != true)
            {
                txtResults.Text = "Please enter a valid IP Address.";
                return;
            }
            //
            // Has a valid IP been entered for the gateway?
            //
            IPAddress theGateway;
            bool gwResult = IPAddress.TryParse(txtGateway.Text, out theGateway);
            if (gwResult != true)
            {
                txtResults.Text = "Please entera valid IP Address for the default gateway.";
                return;
            }
            //
            // Does a vm by this name already exist?
            //
            VimClient vimClient = ConnectServer(Globals.sViServer, Globals.sUsername, Globals.sPassword);
            List<VirtualMachine> chkVirtualMachines = GetVirtualMachines(vimClient, null, txtTargetVm.Text);
            if (chkVirtualMachines != null)
            {
                vimClient.Disconnect();
                txtResults.Text = "virtual machine " + txtTargetVm.Text + " already exists";
                return;
            }
            //
            // Get a list of hosts in the selected cluster
            //
            List<HostSystem> lstHosts = GetHosts(vimClient, cboClusters.SelectedValue);
            //
            // Randomly pick host
            //
            HostSystem selectedHost = lstHosts[rand.Next(0, lstHosts.Count)];
            txtResults.Text = "Host : " + selectedHost.Name + "\r\n";
            //
            // Connect to selected vm to clone
            //
            List<VirtualMachine> lstVirtualMachines = GetVirtualMachines(vimClient, null, cboSourceVms.SelectedItem.Text);
            VirtualMachine itmVirtualMachine = lstVirtualMachines[0];
            txtResults.Text += "Source : " + itmVirtualMachine.Name + "\r\n";
            //
            // Connect to the selected datastore
            //
            List<Datastore> lstDatastores = GetDataStore(vimClient, null, cboDatastores.SelectedItem.Text);
            Datastore itmDatastore = lstDatastores[0];
            txtResults.Text += "Datastore : " + itmDatastore.Name + "\r\n";
            //
            // Connect to portgroup
            //
            List<DistributedVirtualPortgroup> lstDvPortGroups = GetDVPortGroups(vimClient, null, cboPortGroups.SelectedItem.Text);
            DistributedVirtualPortgroup itmDvPortGroup = lstDvPortGroups[0];
            txtResults.Text += "Portgroup : " + itmDvPortGroup.Name + "\r\n";
            //
            // Connect to the customizationspec
            //
            CustomizationSpecItem itmSpecItem = GetCustomizationSpecItem(vimClient, cboCustomizations.SelectedItem.Text);
            txtResults.Text += "Spec : " + cboCustomizations.SelectedItem.Text + "\r\n";
            //
            // Create a new VirtualMachineCloneSpec
            //
            VirtualMachineCloneSpec mySpec = new VirtualMachineCloneSpec();
            mySpec.Location = new VirtualMachineRelocateSpec();
            mySpec.Location.Datastore = itmDatastore.MoRef;
            mySpec.Location.Host = selectedHost.MoRef;
            //
            // Get resource pool for selected cluster
            //
            List<ResourcePool> lstResPools = GetResPools(vimClient, cboClusters.SelectedValue);
            ResourcePool itmResPool = lstResPools[0];
            //
            // Assign resource pool to specitem
            //
            mySpec.Location.Pool = itmResPool.MoRef;
            //
            // Add selected CloneSpec customizations to this CloneSpec
            //
            mySpec.Customization = itmSpecItem.Spec;
            //
            // Handle hostname for either windows or linux
            //
            if (cboCustomizations.SelectedValue == "Windows")
            {
                //
                // Create a windows sysprep object
                //
                CustomizationSysprep winIdent = (CustomizationSysprep)itmSpecItem.Spec.Identity;
                CustomizationFixedName hostname = new CustomizationFixedName();
                hostname.Name = txtTargetVm.Text;
                winIdent.UserData.ComputerName = hostname;
                //
                // Store identity in this CloneSpec
                //
                mySpec.Customization.Identity = winIdent;
            }
            if (cboCustomizations.SelectedValue == "Linux")
            {
                //
                // Create a Linux "sysprep" object
                //
                CustomizationLinuxPrep linIdent = (CustomizationLinuxPrep)itmSpecItem.Spec.Identity;
                CustomizationFixedName hostname = new CustomizationFixedName();
                hostname.Name = txtTargetVm.Text;
                linIdent.HostName = hostname;
                //
                // Store identity in this CloneSpec
                //
                mySpec.Customization.Identity = linIdent;
            }
            //
            // Create a new ConfigSpec
            //
            mySpec.Config = new VirtualMachineConfigSpec();
            //
            // Set number of CPU's
            //
            int numCpus = new int();
            numCpus = Convert.ToInt16(cboCpus.SelectedValue);
            mySpec.Config.NumCPUs = numCpus;
            txtResults.Text += "CPU : " + numCpus + "\r\n";
            //
            // Set amount of RAM
            //
            long memoryMb = new long();
            memoryMb = (long)(Convert.ToInt16(cboRam.SelectedValue) * 1024);
            mySpec.Config.MemoryMB = memoryMb;
            txtResults.Text += "Ram : " + memoryMb + "\r\n";
            //
            // Only handle the first network card
            //
            mySpec.Customization.NicSettingMap = new CustomizationAdapterMapping[1];
            mySpec.Customization.NicSettingMap[0] = new CustomizationAdapterMapping();
            //
            // Read in the DNS from web.config and assign
            //
            string[] ipDns = new string[1];
            ipDns[0] = WebConfigurationManager.AppSettings["dnsServer"].ToString();
            mySpec.Customization.GlobalIPSettings = new CustomizationGlobalIPSettings();
            mySpec.Customization.GlobalIPSettings.DnsServerList = ipDns;
            txtResults.Text += "DNS : " + ipDns[0] + "\r\n";
            //
            // Create a new networkDevice
            //
            VirtualDevice networkDevice = new VirtualDevice();
            foreach (VirtualDevice vDevice in itmVirtualMachine.Config.Hardware.Device)
            {
                //
                // get nic on vm
                //
                if (vDevice.DeviceInfo.Label.Contains("Network"))
                {
                    networkDevice = vDevice;
                }
            }
            //
            // Create a DeviceSpec
            //
            VirtualDeviceConfigSpec[] devSpec = new VirtualDeviceConfigSpec[0];
            mySpec.Config.DeviceChange = new VirtualDeviceConfigSpec[1];
            mySpec.Config.DeviceChange[0] = new VirtualDeviceConfigSpec();
            mySpec.Config.DeviceChange[0].Operation = VirtualDeviceConfigSpecOperation.edit;
            mySpec.Config.DeviceChange[0].Device = networkDevice;
            //
            // Define network settings for the new vm
            //
            //
            // Assign IP Address
            //
            CustomizationFixedIp ipAddress = new CustomizationFixedIp();
            ipAddress.IpAddress = txtIpAddress.Text;
            mySpec.Customization.NicSettingMap[0].Adapter = new CustomizationIPSettings();
            txtResults.Text += "IP : " + txtIpAddress.Text + "\r\n";
            //
            // Assign subnet mask
            //
            mySpec.Customization.NicSettingMap[0].Adapter.Ip = ipAddress;
            mySpec.Customization.NicSettingMap[0].Adapter.SubnetMask = txtSubnet.Text;
            txtResults.Text += "Subnet : " + txtSubnet.Text + "\r\n";
            //
            // Assign default gateway
            //
            string[] ipGateway = new string[1];
            ipGateway[0] = txtGateway.Text;
            mySpec.Customization.NicSettingMap[0].Adapter.Gateway = ipGateway;
            txtResults.Text += "Gateway : " + txtGateway.Text + "\r\n";
            //
            // Create network backing information
            //
            VirtualEthernetCardDistributedVirtualPortBackingInfo nicBack = new VirtualEthernetCardDistributedVirtualPortBackingInfo();
            nicBack.Port = new DistributedVirtualSwitchPortConnection();
            //
            // Connect to the virtual switch
            //
            VmwareDistributedVirtualSwitch dvSwitch = GetDvSwitch(vimClient, itmDvPortGroup.Config.DistributedVirtualSwitch);
            //
            // Assign the proper switch port
            //
            nicBack.Port.SwitchUuid = dvSwitch.Uuid;
            //
            // Connect the network card to proper port group
            //
            nicBack.Port.PortgroupKey = itmDvPortGroup.MoRef.Value;
            mySpec.Config.DeviceChange[0].Device.Backing = nicBack;
            //
            // Enable the network card at bootup
            //
            mySpec.Config.DeviceChange[0].Device.Connectable = new VirtualDeviceConnectInfo();
            mySpec.Config.DeviceChange[0].Device.Connectable.StartConnected = true;
            mySpec.Config.DeviceChange[0].Device.Connectable.AllowGuestControl = true;
            mySpec.Config.DeviceChange[0].Device.Connectable.Connected = true;
            //
            // Get the vmfolder from the datacenter
            //
            List<ClusterComputeResource> lstClusters = GetClusters(vimClient, cboClusters.SelectedItem.Text);
            List<Datacenter> lstDatacenters = GetDcFromCluster(vimClient, lstClusters[0].Parent.Value);
            Datacenter itmDatacenter = lstDatacenters[0];
            //
            // Perform the clone
            //
            itmVirtualMachine.CloneVM_Task(itmDatacenter.VmFolder, txtTargetVm.Text, mySpec);
            vimClient.Disconnect();
            //
            // Hide the vm controls and show the result box
            //
            Vm_Panel.Visible = false;
            Results_Panel.Visible = true;
        }
        protected void cmdClose_Click(object sender, EventArgs e)
        {
            //
            // Hide the reults and show the login panel, clear all form items
            //
            Results_Panel.Visible = false;
            Login_Panel.Visible = true;
        }
        protected void cboClusters_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            // The selected cluster changed so we need to update the following items
            // - Datastore
            // - Portgroups
            // - Resource Pools
            //
            //
            // Establish a connection with the Vmware server
            //
            VimClient vimClient = ConnectServer(Globals.sViServer, Globals.sUsername, Globals.sPassword);
            //
            // Clear out existing entries
            //
            cboDatastores.Items.Clear();
            cboPortGroups.Items.Clear();
            cboResourcePools.Items.Clear();
            //
            // Need to get at the Datacenter for the selected cluster
            //
            List<ClusterComputeResource> lstClusters = GetClusters(vimClient, cboClusters.SelectedItem.Text);
            List<Datacenter> lstDatacenters = GetDcFromCluster(vimClient, lstClusters[0].Parent.Value);
            Datacenter itmDatacenter = lstDatacenters[0];
            //
            // Update datastore list
            //
            List<Datastore> lstDatastores = GetDataStore(vimClient, itmDatacenter);
            lstDatastores = lstDatastores.OrderByDescending(thisStore => thisStore.Info.FreeSpace).ToList();
            foreach (Datastore itmDatastore in lstDatastores)
            {
                ListItem thisDatastore = new ListItem();
                thisDatastore.Text = itmDatastore.Name;
                thisDatastore.Value = itmDatastore.MoRef.Value;
                cboDatastores.Items.Add(thisDatastore);
            }
            //
            // Update list of network portgroups
            //
            List<DistributedVirtualPortgroup> lstDVPortGroups = GetDVPortGroups(vimClient, itmDatacenter);
            if (lstDVPortGroups != null)
            {
                foreach (DistributedVirtualPortgroup itmPortGroup in lstDVPortGroups)
                {
                    ListItem thisPortGroup = new ListItem();
                    thisPortGroup.Text = itmPortGroup.Name;
                    thisPortGroup.Value = itmPortGroup.MoRef.ToString();
                    cboPortGroups.Items.Add(thisPortGroup);
                }
            }
            else
            {
                List<Network> lstPortGroups = GetPortGroups(vimClient, itmDatacenter);
                if (lstPortGroups != null)
                {
                    foreach (Network itmPortGroup in lstPortGroups)
                    {
                        ListItem thisPortGroup = new ListItem();
                        thisPortGroup.Text = itmPortGroup.Name;
                        thisPortGroup.Value = itmPortGroup.MoRef.ToString();
                        cboPortGroups.Items.Add(thisPortGroup);
                    }
                }
            }
            //
            // Get a list of Resource Pools
            //
            List<ResourcePool> lstResPools = GetResPools(vimClient, cboClusters.SelectedItem.Value);
            if (lstResPools != null)
            {
                foreach (ResourcePool itmResPool in lstResPools)
                {
                    ListItem thisResPool = new ListItem();
                    thisResPool.Text = itmResPool.Name;
                    thisResPool.Value = itmResPool.MoRef.ToString();
                    cboResourcePools.Items.Add(thisResPool);
                }
            }
            //
            // Close the session with the server
            //
            vimClient.Disconnect();
        }
    }
}