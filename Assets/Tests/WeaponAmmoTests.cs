using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WeaponAmmoTests
{
    public class Reload
    {
        public TestWeaponSettings Settings { get; private set; }
        public WeaponAmmo Ammo { get; private set; }

        [SetUp]
        public void SetUp ()
        {
            Settings = new TestWeaponSettings();
            Ammo = new WeaponAmmo(Settings);
        }

        [Test]
        public void Full_Clip_Dont_Reload_CurrentAmmo ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(Settings.MaxAmmo);
            Ammo.OverrideRoundsInClip(Settings.RoundsPerClip);
            Ammo.ReloadClip();
            Assert.AreEqual(
                Settings.MaxAmmo,
                Ammo.CurrentAmmo
            );
        }

        [Test]
        public void Full_Clip_Dont_Reload_RoundsInClip ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(Settings.MaxAmmo);
            Ammo.OverrideRoundsInClip(Settings.RoundsPerClip);
            Ammo.ReloadClip();
            Assert.AreEqual(
                Settings.RoundsPerClip,
                Ammo.RoundsInClip
            );
        }

        [Test]
        public void Empty_Clip_Dont_Reload_CurrentAmmo ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(0);
            Ammo.OverrideRoundsInClip(0);
            Ammo.ReloadClip();
            Assert.AreEqual(0, Ammo.CurrentAmmo);
        }

        [Test]
        public void Empty_Clip_Dont_Reload_RoundsInClip ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(0);
            Ammo.OverrideRoundsInClip(0);
            Ammo.ReloadClip();
            Assert.AreEqual(0, Ammo.RoundsInClip);
        }

        [Test]
        public void More_Ammo_Than_PerClip_Half_Clip_Reload_CurrentAmmo ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(Settings.MaxAmmo);
            Ammo.OverrideRoundsInClip(4);
            Ammo.ReloadClip();
            Assert.AreEqual(16, Ammo.CurrentAmmo);
        }

        [Test]
        public void More_Ammo_Than_PerClip_Half_Clip_Reload_RoundsInClip ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(Settings.MaxAmmo);
            Ammo.OverrideRoundsInClip(4);
            Ammo.ReloadClip();
            Assert.AreEqual(8, Ammo.RoundsInClip);
        }

        [Test]
        public void Less_Ammo_Than_PerClip_Half_Clip_Reload_CurrentAmmo ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(4);
            Ammo.OverrideRoundsInClip(2);
            Ammo.ReloadClip();
            Assert.AreEqual(0, Ammo.CurrentAmmo);
        }

        [Test]
        public void Less_Ammo_Than_PerClip_Half_Clip_Reload_RoundsInClip ()
        {
            Settings.MaxAmmo = 20;
            Settings.RoundsPerClip = 8;
            Ammo.OverrideCurrentAmmo(4);
            Ammo.OverrideRoundsInClip(2);
            Ammo.ReloadClip();
            Assert.AreEqual(6, Ammo.RoundsInClip);
        }
    }
}
