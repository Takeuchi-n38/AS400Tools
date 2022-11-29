using System;
namespace Misc.Miscellaneousness.Model.Length4
{
public class WehmInPjik060 {
//  59  "     I            DS"
//  60  "     I                                        1   40WEHM"
//  61  "     I                                        1   20WEHH"
//  62  "     I                                        3   40WEMM"
//  private string sharedString = new string('0', 4);
  private short hh;
  private short mm;
  public void SetValue(string value) {
	  short wehm = short.Parse(value);
      short tmphh = 0;
      if(wehm >= 100) {
    	  tmphh = (short) (wehm / 100);
      }
      hh = tmphh;
      mm = (short) (wehm % 100);
//    Xunit.Assert.True(value.Length == 4);
//    sharedString = value;
  }
  public string GetValue() {
	  return hh.ToString() + mm.ToString();
	  //return sharedString;
  }
  public void SetWehm(short wehm) {
      short tmphh = 0;
      if(wehm >= 100) {
    	  tmphh = (short) (wehm / 100);
      }
      hh = tmphh;
      mm = (short) (wehm % 100);
//    Xunit.Assert.True(wehm.ToString().Length <= 4);
//    sharedString = sharedString.Substring(0, 0) + string.Format("{0:D4}", wehm) + sharedString.Substring(4);
  }
  public short GetWehm() {
	  if(hh != 0) {
		  return short.Parse(hh.ToString() + mm.ToString());
	  }else {
		  return short.Parse(mm.ToString());
	  }
	  //return short.Parse(sharedString.Substring(0, 4));
  }
  public void SetWehh(short wehh) {
	  hh = wehh;
	  //Xunit.Assert.True(wehh.ToString().Length <= 2);
	  //sharedString = sharedString.Substring(0, 0) + string.Format("{0:D2}", wehh) + sharedString.Substring(2);
  }
  public short GetWehh() {
	  return hh;
	  //return short.Parse(sharedString.Substring(0, 2));
  }
  public void SetWemm(short wemm) {
	  mm = wemm;
	  //Xunit.Assert.True(wemm.ToString().Length <= 2);
	  //sharedString = sharedString.Substring(0, 2) + string.Format("{0:D2}", wemm) + sharedString.Substring(4);
  }
  public short GetWemm() {
	  return mm;
	  //return short.Parse(sharedString.Substring(2, 4));
  }
}
}